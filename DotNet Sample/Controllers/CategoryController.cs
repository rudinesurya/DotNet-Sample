using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService CategoryService;
        private readonly IMapper Mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            CategoryService = categoryService;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await CategoryService.GetCategoriesAsync();
            return Ok(Mapper.Map<IEnumerable<ECategory>, IEnumerable<Category>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var category = await CategoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ECategory, Category>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Category category)
        {
            if (category.Id == default)
                category.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await CategoryService.AddAsync(Mapper.Map<Category, ECategory>(category));
            return CreatedAtAction("Add", new { id = category.Id }, category);
        }
    }
}