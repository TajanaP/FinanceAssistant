using FinanceAssistant.Core;
using FinanceAssistant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Persistence
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(FinanceAssistantDbContext dbContext) : base(dbContext)
        {

        }
    }
}
