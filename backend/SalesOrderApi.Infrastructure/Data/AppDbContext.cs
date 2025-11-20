using Microsoft.EntityFrameworkCore;
using SalesOrderApi.Domain.Entities;

namespace SalesOrderApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        public DbSet<SalesOrderLine> SalesOrderLines => Set<SalesOrderLine>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Name = "ABC Traders", Address1 = "No 10", Suburb = "Colombo" },
                new Client { Id = 2, Name = "Kandy Supplies", Address1 = "No 5", Suburb = "Kandy" }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Code = "ITM001", Description = "Screwdriver", Price = 1200M },
                new Item { Id = 2, Code = "ITM002", Description = "Wrench", Price = 1500M }
            );
        }
    }
}
