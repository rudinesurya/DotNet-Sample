using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Controllers.Dto.Cart_Action;
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
            var carts = await DbContext.Carts.Include(c => c.Items).ToListAsync();
            return Ok(Mapper.Map<List<ECart>, List<Cart>>(carts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> Get([FromRoute] Guid id)
        {
            var cart = await DbContext.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ECart, Cart>(cart));
        }

        [HttpPost("AddItem")]
        public async Task<ActionResult> AddItem([FromBody] AddCartItem addCartItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await DbContext.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserName == addCartItem.UserName);

            if (cart == null)
            {
                cart = new ECart
                {
                    UserName = addCartItem.UserName
                };

                await DbContext.Carts.AddAsync(cart);
                await DbContext.SaveChangesAsync();
            }

            cart.Items.Add(new ECartItem
            {
                ProductId = addCartItem.ProductId,
                Quantity = addCartItem.Quantity,
            });

            DbContext.Entry(cart).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();

            return CreatedAtAction("AddItem", new { id = cart.Id }, cart);
        }

        [HttpPost("RemoveItem")]
        public async Task<ActionResult> RemoveItem([FromBody] RemoveCartItem removeCartItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await DbContext.Carts
                       .Include(c => c.Items)
                       .FirstOrDefaultAsync(c => c.Id == removeCartItem.CartId);

            if (cart != null)
            {
                var removedItem = cart.Items.FirstOrDefault(x => x.Id == removeCartItem.CartItemId);
                cart.Items.Remove(removedItem);

                DbContext.Entry(cart).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }

            return NoContent();
        }

        [HttpPost("ClearCart")]
        public async Task<ActionResult> ClearCart([FromBody] Guid cartId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await DbContext.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart != null)
            {
                cart.Items.Clear();
                DbContext.Entry(cart).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}