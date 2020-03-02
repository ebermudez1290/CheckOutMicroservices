using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Common.Repository.Database
{
    public interface IDatabase<T>
    {
        T Create(T entity);
        Task<T> CreateAsync(T entity);

        Task<T> GetByCriteriaAsync(Expression<Func<T, bool>> predicate);
        T GetByCriteria(Expression<Func<T, bool>> predicate);

        T Update(T entity, string id);

        void Delete(T Entity);

        IQueryable<T> ListAll();
    }
}
