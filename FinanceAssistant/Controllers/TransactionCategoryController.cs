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
    [Route("/api/transactionCategory")]
    public class TransactionCategoryController : Controller
    {
        private readonly ITransactionCategoryRepository categoryRepository;
        private readonly ITransactionTypeRepository typeRepository;
        private readonly IMapper mapper;

        public TransactionCategoryController(ITransactionCategoryRepository categoryRepository, ITransactionTypeRepository typeRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.typeRepository = typeRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var categoryInDb = categoryRepository.FindById(id);
            if (categoryInDb == null)
                return NotFound();

            categoryInDb.Type = typeRepository.FindById(categoryInDb.TypeId);
            var categoryViewModel = mapper.Map<TransactionCategory, TransactionCategoryViewModel>(categoryInDb);

            return Ok(categoryViewModel);
        }

        [HttpGet]
        public IEnumerable<TransactionCategoryViewModel> GetCategories()
        {
            var categoriesInDb = categoryRepository.GetAllFromDatabaseEnumerable().ToList();
            foreach (var category in categoriesInDb)
                category.Type = typeRepository.FindById(category.TypeId);

            return mapper.Map<IEnumerable<TransactionCategory>, IEnumerable<TransactionCategoryViewModel>>(categoriesInDb);
        }

        [HttpGet("getCategoriesForType/{id}")]
        public IEnumerable<TransactionCategoryViewModel> GetCategoriesforType(int id)
        {
            var categoriesInDb = categoryRepository.GetAllFromDatabaseEnumerable().Where(c => c.TypeId == id).ToList();
            foreach (var category in categoriesInDb)
                category.Type = typeRepository.FindById(category.TypeId);

            return mapper.Map<IEnumerable<TransactionCategory>, IEnumerable<TransactionCategoryViewModel>>(categoriesInDb);
        }

        [HttpPost]
        public IActionResult Create([FromBody]SaveTransactionCategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = mapper.Map<SaveTransactionCategoryViewModel, TransactionCategory>(categoryViewModel);
            categoryRepository.AddToDatabase(category);
            categoryRepository.Save();

            category.Type = typeRepository.FindById(categoryViewModel.TypeId);
            var result = mapper.Map<TransactionCategory, TransactionCategoryViewModel>(category);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]SaveTransactionCategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryInDb = categoryRepository.FindById(id);
            if (categoryInDb == null)
                return NotFound();

            mapper.Map(categoryViewModel, categoryInDb);
            categoryRepository.Save();

            categoryInDb.Type = typeRepository.FindById(categoryViewModel.TypeId);
            var result = mapper.Map<TransactionCategory, TransactionCategoryViewModel>(categoryInDb);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoryInDb = categoryRepository.FindById(id);
            if (categoryInDb == null)
                return NotFound();

            categoryRepository.DeleteFromDatabase(categoryInDb);
            categoryRepository.Save();

            return Ok(id);
        }
    }
}