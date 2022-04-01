﻿using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers.Service
{
    public interface IProductService
    {
        Task<IEnumerable<EProduct>> GetProductsAsync();

        Task<EProduct> GetProductByIdAsync(Guid id);

        Task<EProduct> AddAsync(EProduct product);
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext DbContext;

        public ProductService(AppDbContext context)
        {
            DbContext = context;
        }

        public async Task<IEnumerable<EProduct>> GetProductsAsync()
        {
            return await DbContext.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<EProduct> GetProductByIdAsync(Guid id)
        {
            return await DbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<EProduct> AddAsync(EProduct product)
        {
            await DbContext.Products.AddAsync(product);
            await DbContext.SaveChangesAsync();
            return product;
        }
    }
}
