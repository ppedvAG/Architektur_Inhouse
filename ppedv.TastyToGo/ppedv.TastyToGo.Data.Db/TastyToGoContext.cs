using Microsoft.EntityFrameworkCore;
using ppedv.TastyToGo.Model;

namespace ppedv.TastyToGo.Data.Db
{
    public class TastyToGoContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        string conString;

        public TastyToGoContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();
        }
    }
}