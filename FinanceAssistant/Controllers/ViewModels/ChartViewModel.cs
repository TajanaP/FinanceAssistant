using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Controllers.ViewModels
{
    public class ChartViewModel
    {
        public List<string> ChartLabels { get; set; }
        public List<decimal> ChartAmounts { get; set; }

        public ChartViewModel()
        {
            ChartLabels = new List<string>();
            ChartAmounts = new List<decimal>();
        }
    }
}
