using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Controllers.ViewModels
{
    public class TransactionCategoryViewModel
    {
        public int Id { get; set; }

        public int TypeId { get; set; } // foreign key
        public TransactionTypeViewModel Type { get; set; }

        public string Name { get; set; }
    }
}
