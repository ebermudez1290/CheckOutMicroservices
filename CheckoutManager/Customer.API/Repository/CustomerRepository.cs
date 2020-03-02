using Service.Common.Repository;
using Service.Common.Repository.Database;
using System.Linq;
using System.Threading.Tasks;
using DbModels = Customer.API.Models;

namespace Customer.API.Repository
{
    public class CustomerRepository : IRepository<DbModels.Customer>
    {
        private IDatabase<DbModels.Customer> _db;
        public CustomerRepository(IDatabase<DbModels.Customer> db)
        {
            _db = db;
        }

        public DbModels.Customer Create(DbModels.Customer customer)
        {
            return _db.Create(customer);
        }

        public async Task<DbModels.Customer> CreateAsync(DbModels.Customer customer)
        {
            return await _db.CreateAsync(customer);
        }

        public async Task<DbModels.Customer> GetByIdAsync(string id)
        {
            return await _db.GetByCriteriaAsync(x=>x.Id == long.Parse(id));
        }

        public DbModels.Customer Update(DbModels.Customer customer)
        {
            return _db.Update(customer, customer.Id.ToString());
        }

        public void Delete(DbModels.Customer customer)
        {
            _db.Delete(customer);
        }

        public IQueryable<DbModels.Customer> ListAllAsync()
        {
            return _db.ListAll();
        }

    }
}
