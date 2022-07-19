using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers.Service
{
    public interface ICategoryService
    {
        IQueryable<Category> GetCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(Guid id);

        Task<Category> AddCategoryAsync(Category category);
    }

    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext DbContext;

        public CategoryService(AppDbContext context)
        {
            DbContext = context;
        }

        public IQueryable<Category> GetCategoriesAsync()
        {
            return DbContext.Categories.AsQueryable();
        }

        public Task<Category> GetCategoryByIdAsync(Guid id) => DbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Category> AddCategoryAsync(Category category)
        {
            await DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();
            return category;
        }
    }
}
