using Checkout.Service.Models;
using Service.Common.Repository;
using Service.Common.Repository.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Service.Repository
{
    public class PaymentRepository : IRepository<Payment>
    {
        private IDatabase<Payment> _db;

        public PaymentRepository(IDatabase<Payment> db)
        {
            _db = db;
        }

        public Payment Create(Payment order)
        {
            return _db.Create(order);
        }

        public async Task<Payment> GetByIdAsync(string id)
        {
            return await _db.GetByCriteriaAsync(x=>x.Id == long.Parse(id));
        }

        public Payment Update(Payment order)
        {
            return _db.Update(order, order.Id.ToString());
        }

        public void Delete(Payment order)
        {
            _db.Delete(order);
        }

        public IQueryable<Payment> ListAllAsync()
        {
            return _db.ListAll();
        }

    }
}
