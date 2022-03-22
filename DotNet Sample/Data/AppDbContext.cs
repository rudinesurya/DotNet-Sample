using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            if (!Categories.Any())
            {
                Categories.AddRange(AppDbContextSeed.GetFixedCategories());
                SaveChangesAsync();
            }

            if (!Products.Any())
            {
                Products.AddRange(AppDbContextSeed.GetFixedProducts());
                SaveChangesAsync();
            }
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Contact> Contacts { get; set; }
    }
}
