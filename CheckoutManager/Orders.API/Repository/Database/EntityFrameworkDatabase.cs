using Microsoft.EntityFrameworkCore;
using Service.Common.Repository.Database;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Orders.API.Repository.Database
{
    public class EntityFrameworkDatabase<T> : IDatabase<T> where T : class
    {
        private DbSet<T> _objectSet;
        private OrderDbContext _dbContext;
        public EntityFrameworkDatabase(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
            _objectSet = _dbContext.Set<T>();
        }

        public T Create(T entity)
        {
            _objectSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> GetByCriteriaAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _objectSet.FirstOrDefaultAsync(predicate);
            }
            catch (System.Exception exception)
            {
                System.Console.WriteLine(exception);
                throw;
            }
        }

        public T Update(T entity, string id)
        {
            _objectSet.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T Entity)
        {
            var result = _objectSet.Remove(Entity);
            if (result.State != EntityState.Deleted)
                throw new Exception("The registry cannot be deleted");
            _dbContext.SaveChanges();
        }

        public IQueryable<T> ListAllAsync()
        {
            return _objectSet;
        }
    }
}
