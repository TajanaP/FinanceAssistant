using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Controllers.ViewModels
{
    public class SaveTransactionCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; } // foreign key

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
