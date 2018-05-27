using AutoMapper;
using FinanceAssistant.Controllers.ViewModels;
using FinanceAssistant.Core;
using FinanceAssistant.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinanceAssistant.Controllers
{
    [Route("/api/transaction")]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransactionCategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public TransactionController(ITransactionRepository transactionRepository, ITransactionCategoryRepository categoryRepository, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.categoryRepository = categoryRepository;
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

            var transactionViewModel = mapper.Map<Transaction, TransactionViewModel>(transactionInDb);
            return Ok(transactionViewModel);
        }

        [HttpGet]
        public IEnumerable<TransactionViewModel> GetTransactions()
        {
            var transactions = transactionRepository.GetAllFromDatabaseEnumerable();

            return mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionViewModel>>(transactions);
        }

        [HttpPost]
        public IActionResult Create([FromBody]TransactionViewModel transactionViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transaction = mapper.Map<TransactionViewModel, Transaction>(transactionViewModel);
            transactionRepository.AddToDatabase(transaction);
            transactionRepository.Save();

            var result = mapper.Map<Transaction, TransactionViewModel>(transaction);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]TransactionViewModel transactionViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transactionInDb = transactionRepository.FindById(id);
            if (transactionInDb == null)
                return NotFound();

            mapper.Map(transactionViewModel, transactionInDb);
            transactionRepository.Save();

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

            return Ok();
        }
    }
}