using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService ProductService;
        private readonly IMapper Mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            ProductService = productService;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await ProductService.GetProductsAsync();
            return Ok(Mapper.Map<IEnumerable<EProduct>, IEnumerable<Product>>(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var product = await ProductService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EProduct, Product>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            if (product.Id == default)
                product.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await ProductService.AddAsync(Mapper.Map<Product, EProduct>(product));

            return CreatedAtAction("Add", new { id = product.Id }, product);
        }
    }
}
