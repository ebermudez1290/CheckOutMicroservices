using System.Linq;
using System.Threading.Tasks;

namespace Service.Common.Repository
{
    public interface IRepository<T>
    {
        T Create(T entity);
        Task<T> GetByIdAsync(string id);
        T Update(T entity);
        void Delete(T entity);
        IQueryable<T> ListAllAsync();
    }
}
