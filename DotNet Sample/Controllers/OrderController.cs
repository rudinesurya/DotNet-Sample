using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext DbContext;

        public OrderController(AppDbContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return Ok(await DbContext.Orders.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(Guid id)
        {
            var order = await DbContext.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}