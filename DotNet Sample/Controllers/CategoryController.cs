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

        [HttpGet(Name = "GetCategories")]
        [ProducesResponseType(typeof(IList<Category>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var categories = await CategoryService.GetCategoriesAsync();
            return Ok(Mapper.Map<IList<ECategory>, IList<Category>>(categories));
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var category = await CategoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ECategory, Category>(category));
        }

        [HttpPost(Name = "AddCategory")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] Category category)
        {
            if (category.Id == default)
                category.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await CategoryService.AddCategoryAsync(Mapper.Map<Category, ECategory>(category));
            return CreatedAtAction("Add", new { id = category.Id }, category);
        }
    }
}