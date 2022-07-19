using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers.Service
{
    public interface IOrderService
    {
        IQueryable<Order> GetOrdersAsync();

        Task<Order> GetOrderByIdAsync(Guid id);

        Task<Order> GetOrderByCartIdAsync(Guid cartId);

        Task<IList<Order>> GetOrdersByUserNameAsync(string userName);

        Task<Order> AddOrderAsync(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDbContext DbContext;

        public OrderService(AppDbContext context)
        {
            DbContext = context;
        }

        public IQueryable<Order> GetOrdersAsync()
        {
            return DbContext.Orders.AsQueryable();
        }

        public Task<Order> GetOrderByIdAsync(Guid id) => DbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

        public Task<Order> GetOrderByCartIdAsync(Guid cartId) => DbContext.Orders.FirstOrDefaultAsync(o => o.CartId == cartId);

        public async Task<IList<Order>> GetOrdersByUserNameAsync(string userName)
        {
            return await DbContext.Orders.Where(o => o.UserName == userName).ToListAsync();
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await DbContext.Orders.AddAsync(order);
            await DbContext.SaveChangesAsync();
            return order;
        }
    }
}
