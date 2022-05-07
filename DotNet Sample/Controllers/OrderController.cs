using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService OrderService;
        private readonly IMapper Mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            OrderService = orderService;
            Mapper = mapper;
        }

        [HttpGet(Name = "GetOrders")]
        [ProducesResponseType(typeof(IList<Order>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var orders = await OrderService.GetOrdersAsync();
            return Ok(Mapper.Map<IList<EOrder>, IList<Order>>(orders));
        }

        [HttpGet("username/{userName}", Name = "GetOrdersByUserName")]
        [ProducesResponseType(typeof(IList<Order>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] string userName)
        {
            var orders = await OrderService.GetOrdersByUserNameAsync(userName);
            return Ok(Mapper.Map<IList<EOrder>, IList<Order>>(orders));
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var order = await OrderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EOrder, Order>(order));
        }
    }
}