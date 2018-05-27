using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Controllers.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; } // foreign key
        public TransactionTypeViewModel Type { get; set; }

        [Required]
        public int CategoryId { get; set; } // foreign key
        public TransactionCategoryViewModel Category { get; set; }

        public string Description { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
