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

        public TransactionTypeViewModel Type { get; set; }

        public string Name { get; set; }

        public decimal ChartAmount { get; set; }
    }
}
