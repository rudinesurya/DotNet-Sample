using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext DbContext;

        public CategoryController(AppDbContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return Ok(await DbContext.Categories.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(Guid id)
        {
            var category = await DbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
    }
}