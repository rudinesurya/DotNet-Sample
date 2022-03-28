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
    public class CartController : ControllerBase
    {
        private readonly AppDbContext DbContext;
        private readonly IMapper Mapper;

        public CartController(AppDbContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> Get()
        {
            var carts = await DbContext.Carts.ToListAsync();
            return Ok(Mapper.Map<List<ECart>, List<Cart>>(carts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> Get(Guid id)
        {
            var cart = await DbContext.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ECart, Cart>(cart));
        }
    }
}