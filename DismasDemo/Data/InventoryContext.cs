using DismasDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DismasDemo.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) 
        {
        }

        public DbSet<Part> Part { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceList>().ToTable("PriceList");
        }

        public DbSet<DismasDemo.Models.Price> Price { get; set; }
    }
}
