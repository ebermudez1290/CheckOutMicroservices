using Microsoft.EntityFrameworkCore;
using customerModels= Customer.API.Models;

namespace Customer.API.Database
{
    public class CustomerDbContext: DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        public DbSet<customerModels.Customer> Customer { get; set; }
        public DbSet<customerModels.Document> Document { get; set; }
    }
}
