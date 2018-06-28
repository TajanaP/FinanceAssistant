using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using FinanceAssistant.Controllers.ViewModels;
using FinanceAssistant.Core;
using FinanceAssistant.Core.Models;
using FinanceAssistant.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAssistant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransactionCategoryRepository categoryRepository;
        private readonly ITransactionTypeRepository typeRepository;
        private readonly IMapper mapper;

        public HomeController(ITransactionRepository transactionRepository, ITransactionCategoryRepository categoryRepository, ITransactionTypeRepository typeRepository, IMapper mapper)
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

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        // ExchangeRate

        [HttpGet("sum/{currencyToDisplay}/{startDate?}/{endDate?}")]
        public decimal CalculateSum(string currencyToDisplay, DateTime? startDate = null, DateTime? endDate = null)
        {
            decimal sum = 0;
            var transactions = GetTransactions(startDate, endDate);
            foreach (var transaction in transactions)
            {
                var amount = CalculateTransactionAmountForChosenCurrency(transaction, currencyToDisplay);

                if (transaction.Category.Type.Id == 1)
                    sum = sum - amount;
                else
                    sum = sum + amount;
            }
            return decimal.Round(sum, 2);
        }

        public decimal CalculateExchangeRate(DateTime date, string currency)
        {
            var exchangeRateList = new ExchangeRate(date.ToString("yyyy-MM-dd"));
            ExchangeRate.item exchangeRateEUR = exchangeRateList.getExchangeRate("EUR");
            var rateEUR = decimal.Parse(exchangeRateEUR.jedinica) * decimal.Parse(exchangeRateEUR.srednji_tecaj);
            return rateEUR;
        }

        public decimal CalculateTransactionAmountForChosenCurrency(TransactionViewModel transaction, string currencyToDisplay)
        {
            decimal amount = 0;
            if (currencyToDisplay == "EUR")
            {
                if (transaction.Currency != "EUR")
                {
                    var exchangeRate = CalculateExchangeRate(transaction.Date, transaction.Currency);
                    amount = transaction.Amount / exchangeRate; // convert to EUR
                }
                else
                    amount = transaction.Amount;
            }
            else
            {
                if (transaction.Currency != "HRK")
                {
                    var exchangeRate = CalculateExchangeRate(transaction.Date, transaction.Currency);
                    amount = transaction.Amount * exchangeRate; // convert to HRK
                }
                else
                    amount = transaction.Amount;
            }
            return amount;
        }

        // Charts

        [HttpGet("chartDataByCategory/{currencyToDisplay}/{id?}/{startDate?}/{endDate?}")]
        public ChartViewModel GetChartDataByCategory(string currencyToDisplay, int? id = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var sortedCategories = new List<TransactionCategoryViewModel>();
            var chartData = new ChartViewModel();

            var categories = GetCategories(id);
            foreach (var category in categories)
            {
                var transactions = GetTransactions(startDate, endDate).Where(t => t.Category.Id == category.Id);
                foreach (var transaction in transactions)
                {
                    var amount = CalculateTransactionAmountForChosenCurrency(transaction, currencyToDisplay);
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

        [HttpGet("chartDataByType/{currencyToDisplay}/{startDate?}/{endDate?}")]
        public ChartViewModel GetChartDataByType(string currencyToDisplay, DateTime? startDate = null, DateTime? endDate = null)
        {
            var chartData = new ChartViewModel();
            decimal amount = 0;

            var types = GetTypes();
            foreach (var type in types)
            {
                var transactions = GetTransactions(startDate, endDate).Where(t => t.Category.Type.Id == type.Id);
                foreach (var transaction in transactions)
                    amount += CalculateTransactionAmountForChosenCurrency(transaction, currencyToDisplay);

                chartData.ChartLabels.Add(type.Name);
                chartData.ChartAmounts.Add(amount);
                amount = 0;
            }
            return chartData;
        }

        // Helper Methods For Data Access

        public IEnumerable<TransactionViewModel> GetTransactions(DateTime? startDate, DateTime? endDate)
        {
            var transactions = new List<Transaction>();

            if (startDate != null && endDate != null)
                transactions = transactionRepository.GetAllFromDatabaseEnumerable().Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
            else
                transactions = transactionRepository.GetAllFromDatabaseEnumerable().ToList();

            foreach (var transaction in transactions)
            {
                transaction.Category = categoryRepository.FindById(transaction.CategoryId);
                transaction.Category.Type = typeRepository.FindById(transaction.TypeId);
            }

            return mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionViewModel>>(transactions);
        }

        public IEnumerable<TransactionCategoryViewModel> GetCategories(int? id)
        {
            IEnumerable<TransactionCategory> categoriesInDb;

            if (id != null)
                categoriesInDb = categoryRepository.GetAllFromDatabaseEnumerable().Where(c => c.TypeId == id).ToList();
            else
                categoriesInDb = categoryRepository.GetAllFromDatabaseEnumerable().ToList();

            foreach (var category in categoriesInDb)
                category.Type = typeRepository.FindById(category.TypeId);

            return mapper.Map<IEnumerable<TransactionCategory>, IEnumerable<TransactionCategoryViewModel>>(categoriesInDb);
        }

        public IEnumerable<TransactionTypeViewModel> GetTypes()
        {
            var transactionTypes = typeRepository.GetAllFromDatabaseEnumerable().ToList();
            return Mapper.Map<List<TransactionType>, List<TransactionTypeViewModel>>(transactionTypes);
        }
    }
}
