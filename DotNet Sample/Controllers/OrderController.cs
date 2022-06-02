using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNet_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ODataController
    {
        private readonly IOrderService OrderService;

        public OrderController(IOrderService orderService)
        {
            OrderService = orderService;
        }

        [HttpGet(Name = "GetOrders")]
        [EnableQuery]
        [ProducesResponseType(typeof(IList<Order>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var orders = await OrderService.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        [EnableQuery]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var order = await OrderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}