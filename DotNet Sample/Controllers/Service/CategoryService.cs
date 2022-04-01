using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers.Service
{
    public interface ICategoryService
    {
        Task<ECategory> AddAsync(ECategory category);

        Task<IEnumerable<ECategory>> GetCategoriesAsync();

        Task<ECategory> GetCategoryByIdAsync(Guid id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext DbContext;

        public CategoryService(AppDbContext context)
        {
            DbContext = context;
        }

        public async Task<IEnumerable<ECategory>> GetCategoriesAsync()
        {
            return await DbContext.Categories.ToListAsync();
        }

        public async Task<ECategory> GetCategoryByIdAsync(Guid id)
        {
            return await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ECategory> AddAsync(ECategory category)
        {
            await DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();
            return category;
        }
    }
}
