using FinanceAssistant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Core
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
    }
}
