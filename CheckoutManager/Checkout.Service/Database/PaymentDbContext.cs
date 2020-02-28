using Checkout.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Service.Database
{
    public class PaymentDbContext: DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payment { get; set; }
    }
}
