using Microsoft.EntityFrameworkCore;
using Orders.API.Models;

namespace Orders.API.Repository.Database
{
    public class OrderDbContext: DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
