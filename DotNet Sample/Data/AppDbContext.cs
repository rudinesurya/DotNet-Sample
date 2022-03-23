using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.OpenConnection();
            if (Database.EnsureCreated())
            {
                // Seed Categories
                var categories = AppDbContextSeed.GetFixedCategories();
                Categories.AddRange(categories);

                // Seed Products
                Products.AddRange(AppDbContextSeed.GetFixedProducts(categories));

                SaveChangesAsync();
            }
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
