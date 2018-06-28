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
    
        [HttpGet("all")]
        public IEnumerable<TransactionViewModel> GetTransactions()
        {
            var transactions = transactionRepository.GetAllFromDatabaseEnumerable().ToList();
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

        public void SetTransactionTypeAndCategory(Transaction transaction)
        {
            transaction.Category = categoryRepository.FindById(transaction.CategoryId);
            transaction.Category.Type = typeRepository.FindById(transaction.TypeId);
        }
    }
}