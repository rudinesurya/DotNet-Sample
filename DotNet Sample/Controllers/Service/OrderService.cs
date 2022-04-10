using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers.Service
{
    public interface IOrderService
    {
        Task<EOrder> GetOrderByIdAsync(Guid id);

        Task<EOrder> GetOrderByCartIdAsync(Guid cartId);

        Task<IEnumerable<EOrder>> GetOrdersByUserNameAsync(string userName);

        Task<IEnumerable<EOrder>> GetOrdersAsync();

        Task<EOrder> AddOrderAsync(EOrder order);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDbContext DbContext;

        public OrderService(AppDbContext context)
        {
            DbContext = context;
        }

        public async Task<IEnumerable<EOrder>> GetOrdersAsync()
        {
            return await DbContext.Orders.ToListAsync();
        }

        public async Task<IEnumerable<EOrder>> GetOrdersByUserNameAsync(string userName)
        {
            return await DbContext.Orders.Where(o => o.UserName == userName).ToListAsync();
        }

        public async Task<EOrder> GetOrderByIdAsync(Guid id)
        {
            return await DbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<EOrder> GetOrderByCartIdAsync(Guid cartId)
        {
            return await DbContext.Orders.FirstOrDefaultAsync(o => o.CartId == cartId);
        }

        public async Task<EOrder> AddOrderAsync(EOrder order)
        {
            await DbContext.Orders.AddAsync(order);
            await DbContext.SaveChangesAsync();
            return order;
        }
    }
}
