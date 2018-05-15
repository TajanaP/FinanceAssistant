using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Controllers.ViewModels
{
    public class TransactionCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; } // foreign key
        public TransactionTypeViewModel Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
