using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Controllers.ViewModels
{
    public class SaveTransactionViewModel
    {
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; } // foreign key

        [Required]
        public int CategoryId { get; set; } // foreign key

        public string Description { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
