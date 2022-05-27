using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers.Service
{
    public interface IProductService
    {
        Task<IList<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(Guid id);

        Task<Product> AddProductAsync(Product product);
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext DbContext;

        public ProductService(AppDbContext context)
        {
            DbContext = context;
        }

        public async Task<IList<Product>> GetProductsAsync()
        {
            return await DbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await DbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await DbContext.Products.AddAsync(product);
            await DbContext.SaveChangesAsync();
            return product;
        }
    }
}
