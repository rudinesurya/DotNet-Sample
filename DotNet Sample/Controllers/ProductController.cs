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
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext DbContext;
        private readonly IMapper Mapper;

        public ProductController(AppDbContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await DbContext.Products.ToListAsync();
            return Ok(Mapper.Map<List<EProduct>, List<Product>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            var product = await DbContext.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EProduct, Product>(product));
        }
    }
}
