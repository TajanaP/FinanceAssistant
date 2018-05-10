using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Core.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; } // foreign key
        public TransactionType Type { get; set; }

        [Required]
        public int CategoryId { get; set; } // foreign key
        public TransactionCategory Category { get; set; }

        public string Description { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
