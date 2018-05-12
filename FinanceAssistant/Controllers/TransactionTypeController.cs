using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinanceAssistant.Controllers.ViewModels;
using FinanceAssistant.Core;
using FinanceAssistant.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAssistant.Controllers
{
    public class TransactionTypeController : Controller
    {
        private readonly ITransactionTypeRepository transactionTypeRepository;
        private readonly IMapper mapper;

        public TransactionTypeController(ITransactionTypeRepository transactionTypeRepository, IMapper mapper)
        {
            this.transactionTypeRepository = transactionTypeRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/transactionTypes")]
        public IEnumerable<TransactionTypeViewModel> GetTypes()
        {
            var transactionTypes = transactionTypeRepository.GetAllFromDatabaseEnumerable().ToList();
            return Mapper.Map<List<TransactionType>, List<TransactionTypeViewModel>>(transactionTypes);
        }
    }
}