using Orders.API.Models;
using Service.Common.Repository;
using Service.Common.Repository.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private IDatabase<Order> _db;
        public OrderRepository(IDatabase<Order> db)
        {
            _db = db;
        }

        public Order Create(Order order)
        {
            return _db.Create(order);
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await _db.GetByCriteriaAsync(x=>x.Id == long.Parse(id));
        }

        public Order Update(Order order)
        {
            return _db.Update(order, order.Id.ToString());
        }

        public void Delete(Order order)
        {
            _db.Delete(order);
        }

        public IQueryable<Order> ListAllAsync()
        {
            return _db.ListAllAsync();
        }

    }
}
