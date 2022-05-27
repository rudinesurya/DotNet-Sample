using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace DotNet_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService CategoryService;

        public CategoryController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [HttpGet(Name = "GetCategories")]
        [EnableQuery]
        [ProducesResponseType(typeof(IList<Category>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var categories = await CategoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        [EnableQuery]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var category = await CategoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
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

            await CategoryService.AddCategoryAsync(category);
            return CreatedAtAction("Add", new { id = category.Id }, category);
        }
    }
}