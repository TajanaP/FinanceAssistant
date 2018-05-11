using FinanceAssistant.Core;
using FinanceAssistant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Persistence
{
    public class TransactionTypeRepository : BaseRepository<TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(FinanceAssistantDbContext dbContext) : base(dbContext)
        {
        }
    }
}
