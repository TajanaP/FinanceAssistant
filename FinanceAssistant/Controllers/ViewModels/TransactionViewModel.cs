using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Controllers.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        public int TypeId { get; set; } // foreign key
        public TransactionTypeViewModel Type { get; set; }

        public int CategoryId { get; set; } // foreign key
        public TransactionCategoryViewModel Category { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public string Currency { get; set; }

        public DateTime Date { get; set; }
    }
}
