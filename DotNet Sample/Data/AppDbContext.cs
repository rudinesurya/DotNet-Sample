using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                // Seed Categories
                var categories = AppDbContextSeed.GetFixedCategories();
                Categories.AddRange(categories);

                // Seed Products
                Products.AddRange(AppDbContextSeed.GetFixedProducts());

                SaveChangesAsync();
            }
        }

        public DbSet<EProduct> Products { get; set; }

        public DbSet<ECategory> Categories { get; set; }

        public DbSet<ECart> Carts { get; set; }

        public DbSet<ECartItem> CartItems { get; set; }

        public DbSet<EOrder> Orders { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
