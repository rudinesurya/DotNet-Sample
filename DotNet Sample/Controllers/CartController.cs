using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Controllers.Dto.Cart_Action;
using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService CartService;
        private readonly IOrderService OrderService;
        private readonly IMapper Mapper;

        public CartController(ICartService cartService, IOrderService orderService, IMapper mapper)
        {
            CartService = cartService;
            OrderService = orderService;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var carts = await CartService.GetCartsAsync();
            return Ok(Mapper.Map<IEnumerable<ECart>, IEnumerable<Cart>>(carts));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var cart = await CartService.GetCartByIdAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ECart, Cart>(cart));
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] AddCartItem addCartItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await CartService.AddItemAsync(addCartItem.UserName, addCartItem.ProductId, addCartItem.Quantity);

            return CreatedAtAction("AddItem", new { id = cart.Id }, addCartItem);
        }

        [HttpPost("RemoveItem")]
        public async Task<IActionResult> RemoveItem([FromBody] RemoveCartItem removeCartItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await CartService.RemoveItemAsync(removeCartItem.CartId, removeCartItem.CartItemId);

            return NoContent();
        }

        [HttpPost("ClearCart")]
        public async Task<IActionResult> ClearCart([FromBody] Guid cartId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await CartService.ClearCartAsync(cartId);

            return NoContent();
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> CheckoutCart([FromBody] Guid cartId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await OrderService.GetOrderByCartIdAsync(cartId);
            if (order == null)
            {
                var cart = await CartService.GetCartByIdAsync(cartId);

                var newOrder = new EOrder() {
                    Id = Guid.NewGuid(),
                    CartId = cartId,
                    UserName = cart.UserName,
                    TotalPrice = cart.TotalPrice,
                    FirstName = "",
                    LastName = "",
                    EmailAddress = "",
                    AddressLine = "",
                    Country = "",
                    State = "",
                    ZipCode = "",
                    CardName = "",
                    CardNumber = "",
                    Expiration = "",
                    CVV = "",
                    PaymentMethod = Entity.PaymentMethod.Paypal,
                };
                order = await OrderService.AddOrderAsync(newOrder);
            }

            return CreatedAtAction("CheckoutCart", new { id = order.Id }, order);
        }
    }
}