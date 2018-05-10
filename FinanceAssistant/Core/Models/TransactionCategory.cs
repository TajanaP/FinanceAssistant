using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Core.Models
{
    public class TransactionCategory
    {
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; } // foreign key
        public TransactionType Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
