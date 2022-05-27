using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers.Service
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(Guid id);

        Task<IList<Order>> GetOrdersAsync();

        Task<Order> AddOrderAsync(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDbContext DbContext;

        public OrderService(AppDbContext context)
        {
            DbContext = context;
        }

        public async Task<IList<Order>> GetOrdersAsync()
        {
            return await DbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await DbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await DbContext.Orders.AddAsync(order);
            await DbContext.SaveChangesAsync();
            return order;
        }
    }
}
