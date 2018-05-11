using FinanceAssistant.Core;
using FinanceAssistant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Persistence
{
    public class TransactionCategoryRepository : BaseRepository<TransactionCategory>, ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(FinanceAssistantDbContext dbContext) : base(dbContext)
        {

        }
    }
}
