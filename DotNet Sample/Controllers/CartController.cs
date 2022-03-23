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

        public CartController(AppDbContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> Get()
        {
            return Ok(await DbContext.Carts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> Get(Guid id)
        {
            var cart = await DbContext.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }
    }
}