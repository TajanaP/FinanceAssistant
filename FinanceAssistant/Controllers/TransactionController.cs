using AutoMapper;
using FinanceAssistant.Controllers.ViewModels;
using FinanceAssistant.Core;
using FinanceAssistant.Core.Models;
using FinanceAssistant.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceAssistant.Controllers
{
    [Route("/api/transaction")]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransactionCategoryRepository categoryRepository;
        private readonly ITransactionTypeRepository typeRepository;
        private readonly IMapper mapper;

        public TransactionController(ITransactionRepository transactionRepository, ITransactionCategoryRepository categoryRepository, ITransactionTypeRepository typeRepository, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.categoryRepository = categoryRepository;
            this.typeRepository = typeRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            var transactionInDb = transactionRepository.FindById(id);
            if (transactionInDb == null)
                return NotFound();

            SetTransactionTypeAndCategory(transactionInDb);
            var transactionViewModel = mapper.Map<Transaction, TransactionViewModel>(transactionInDb);

            return Ok(transactionViewModel);
        }
    
        [HttpGet("all/{startDate?}/{endDate?}")]
        public IEnumerable<TransactionViewModel> GetTransactions(DateTime? startDate, DateTime? endDate)
        {
            var transactions = new List<Transaction>();

            if (startDate != null && endDate != null)
                transactions = transactionRepository.GetAllFromDatabaseEnumerable().Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
            else
                transactions = transactionRepository.GetAllFromDatabaseEnumerable().ToList();

            foreach (var transaction in transactions)
                SetTransactionTypeAndCategory(transaction);

            return mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionViewModel>>(transactions);
        }

        [HttpPost]
        public IActionResult Create([FromBody]SaveTransactionViewModel transactionViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transaction = mapper.Map<SaveTransactionViewModel, Transaction>(transactionViewModel);
            transactionRepository.AddToDatabase(transaction);
            transactionRepository.Save();

            SetTransactionTypeAndCategory(transaction);
            var result = mapper.Map<Transaction, TransactionViewModel>(transaction);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]SaveTransactionViewModel transactionViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transactionInDb = transactionRepository.FindById(id);
            if (transactionInDb == null)
                return NotFound();

            mapper.Map(transactionViewModel, transactionInDb);
            transactionRepository.Save();

            SetTransactionTypeAndCategory(transactionInDb);
            var result = mapper.Map<Transaction, TransactionViewModel>(transactionInDb);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var transactionInDb = transactionRepository.FindById(id);
            if (transactionInDb == null)
                return NotFound();

            transactionRepository.DeleteFromDatabase(transactionInDb);
            transactionRepository.Save();

            return Ok(id);
        }

        // Helper Methods

        public void SetTransactionTypeAndCategory(Transaction transaction)
        {
            transaction.Category = categoryRepository.FindById(transaction.CategoryId);
            transaction.Category.Type = typeRepository.FindById(transaction.TypeId);
        }

        [HttpGet("exchangeRate/{date}/{currency}/{amount}")]
        public decimal CalculateExchangeRate(DateTime date, string currency, decimal amount)
        {
            var exchangeRateList = new ExchangeRate(date.ToString("yyyy-MM-dd"));
      
            if (currency != "EUR")
            {
                ExchangeRate.item exchangeRateEUR = exchangeRateList.getExchangeRate("EUR");
                var rateEUR = decimal.Parse(exchangeRateEUR.jedinica) * decimal.Parse(exchangeRateEUR.srednji_tecaj);
                return amount / rateEUR;
            }
            return amount;
        }

        [HttpGet("sum")]
        public decimal CalculateSum()
        {
            decimal sum = 0;
            var transactions = GetTransactions(null, null);
            foreach (var transaction in transactions)
            {
                var amount = CalculateExchangeRate(transaction.Date, transaction.Currency, transaction.Amount);
                if (transaction.Category.Type.Id == 1)
                    sum = sum - amount;
                else
                    sum = sum + amount;
            }
            return decimal.Round(sum, 2);
        }

        // Charts

        [HttpGet("chartDataByCategory/{id?}/{startDate?}/{endDate?}")]
        public ChartViewModel GetChartDataByCategory(int? id = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var categoriesInDb = new List<TransactionCategory>();
            var sortedCategories = new List<TransactionCategoryViewModel>();
            var chartData = new ChartViewModel();

            if (id != null)
                categoriesInDb = categoryRepository.GetAllFromDatabaseEnumerable().Where(c => c.TypeId == id).ToList();
            else
                categoriesInDb = categoryRepository.GetAllFromDatabaseEnumerable().ToList();

            var categories = mapper.Map<List<TransactionCategory>, List<TransactionCategoryViewModel>>(categoriesInDb);
            foreach (var category in categories)
            {
                var transactions = GetTransactions(startDate, endDate).Where(t => t.Category.Id == category.Id);
                foreach (var transaction in transactions)
                {
                    var amount = CalculateExchangeRate(transaction.Date, transaction.Currency, transaction.Amount);
                    category.ChartAmount += amount;
                    category.Type = transaction.Category.Type;
                }

                if (category.ChartAmount != 0 && id == null) // without ID --> income + expense
                    sortedCategories.Add(category);
                else if (category.ChartAmount != 0 && category.Type.Id == id) // with ID --> income OR expense
                    sortedCategories.Add(category);
            }

            foreach (var category in sortedCategories)
            {
                chartData.ChartLabels.Add(category.Name);
                chartData.ChartAmounts.Add(category.ChartAmount);
            }
            return chartData;
        }

        [HttpGet("chartDataByType/{startDate?}/{endDate?}")]
        public ChartViewModel GetChartDataByType(DateTime? startDate = null, DateTime? endDate = null)
        {
            var chartData = new ChartViewModel();
            decimal amount = 0;

            var typesInDb = typeRepository.GetAllFromDatabaseEnumerable().ToList();
            var types = mapper.Map<List<TransactionType>, List<TransactionTypeViewModel>>(typesInDb);
            foreach (var type in types)
            {
                var transactions = GetTransactions(startDate, endDate).Where(t => t.Category.Type.Id == type.Id);
                foreach (var transaction in transactions)
                    amount += CalculateExchangeRate(transaction.Date, transaction.Currency, transaction.Amount);

                chartData.ChartLabels.Add(type.Name);
                chartData.ChartAmounts.Add(amount);
                amount = 0;
            }
            return chartData;
        }
    }
}