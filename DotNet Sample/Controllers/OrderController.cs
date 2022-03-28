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
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext DbContext;
        private readonly IMapper Mapper;

        public OrderController(AppDbContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var orders = await DbContext.Orders.ToListAsync();
            return Ok(Mapper.Map<List<EOrder>, List<Order>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(Guid id)
        {
            var order = await DbContext.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EOrder, Order>(order));
        }
    }
}