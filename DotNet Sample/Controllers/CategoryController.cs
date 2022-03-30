using AutoMapper;
using DotNet_Sample.Controllers.Dto;
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
        private readonly IMapper Mapper;

        public CategoryController(AppDbContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categories = await DbContext.Categories.ToListAsync();
            return Ok(Mapper.Map<List<ECategory>, List<Category>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get([FromRoute] Guid id)
        {
            var category = await DbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ECategory, Category>(category));
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Category category)
        {
            if (category.Id == default)
                category.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = await DbContext.Categories.AddAsync(Mapper.Map<Category, ECategory>(category));
            await DbContext.SaveChangesAsync();

            return CreatedAtAction("Add", new { id = category.Id }, category);
        }
    }
}