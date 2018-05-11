using FinanceAssistant.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceAssistant.Persistence
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly FinanceAssistantDbContext _dbContext;

        public BaseRepository(FinanceAssistantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TEntity> GetAllFromDatabaseEnumerable() => _dbContext.Set<TEntity>().AsEnumerable();

        public IQueryable<TEntity> GetAllFromDatabaseQueryable() => _dbContext.Set<TEntity>().AsQueryable();

        public TEntity FindById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void AddToDatabase(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddOrUpdateInDatabase(TEntity entity)
        {
            TEntity baseEntity = _dbContext.Set<TEntity>().Find(entity);
            if (baseEntity == null)
                AddToDatabase(entity);
            else
                UpdateInDatabase(entity);
        }

        public void UpdateInDatabase(TEntity entity)
        {
            var baseEntity = _dbContext.Set<TEntity>().Find(entity);
            _dbContext.Entry(baseEntity).CurrentValues.SetValues(entity);
        }

        public void UpdateInDatabase(TEntity entity, int id)
        {
            var baseEntity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Entry(baseEntity).CurrentValues.SetValues(entity);
        }

        public void DeleteFromDatabase(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
