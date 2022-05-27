using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers.Service
{
    public interface ICategoryService
    {
        Task<Category> AddCategoryAsync(Category category);

        Task<IList<Category>> GetCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(Guid id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext DbContext;

        public CategoryService(AppDbContext context)
        {
            DbContext = context;
        }

        public async Task<IList<Category>> GetCategoriesAsync()
        {
            return await DbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            await DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();
            return category;
        }
    }
}
