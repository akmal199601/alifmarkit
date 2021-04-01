using Microsoft.EntityFrameworkCore;

namespace AlifAdminMiniMarketV2.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public virtual DbSet<ProductCategory> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<ProductCategory>().HasData(
            new ProductCategory()
            {
                Id = 1,
                Category = "Фрукты"
            },
            new ProductCategory()
            {
                Id = 2,
                Category = "Овощи"
            },
            new ProductCategory()
            {
                Id = 3,
                Category = "Мясо"
            });
        }
    }
}