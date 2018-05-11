using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Core
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAllFromDatabaseQueryable();
        IEnumerable<TEntity> GetAllFromDatabaseEnumerable();
        TEntity FindById(int id);
        void AddToDatabase(TEntity entity);
        void AddOrUpdateInDatabase(TEntity entity);
        void UpdateInDatabase(TEntity entity);
        void UpdateInDatabase(TEntity entity, int id);
        void DeleteFromDatabase(TEntity entity);
        void Save();
    }
}
