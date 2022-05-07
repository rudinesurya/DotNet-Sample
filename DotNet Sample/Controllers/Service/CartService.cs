using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Sample.Controllers.Service
{
    public interface ICartService
    {
        Task<ECart> AddItemAsync(string userName, Guid productId, int quantity);

        Task<ECart> ClearCartAsync(Guid cartId);

        Task<ECart> GetCartByIdAsync(Guid id);

        Task<ECart> GetCartByUserNameAsync(string userName);

        Task<IList<ECart>> GetCartsAsync();

        Task<ECart> RemoveItemAsync(Guid cartId, Guid cartItemId);
    }

    public class CartService : ICartService
    {
        private readonly AppDbContext DbContext;

        public CartService(AppDbContext context)
        {
            DbContext = context;
        }

        public async Task<IList<ECart>> GetCartsAsync()
        {
            return await DbContext.Carts.Include(c => c.Items).ToListAsync();
        }

        public async Task<ECart> GetCartByIdAsync(Guid id)
        {
            return await DbContext.Carts.Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ECart> GetCartByUserNameAsync(string userName)
        {
            return await DbContext.Carts.Include(c => c.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Category).FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public async Task<ECart> AddItemAsync(string userName, Guid productId, int quantity)
        {
            var cart = await DbContext.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserName == userName);

            if (cart == null)
            {
                cart = new ECart
                {
                    UserName = userName
                };

                await DbContext.Carts.AddAsync(cart);
                await DbContext.SaveChangesAsync();
            }

            // check for existing product already in cart
            var sameCartItem = cart.Items.FirstOrDefault(x => x.ProductId == productId);
            if (sameCartItem != null)
            {
                sameCartItem.Quantity += quantity;
                DbContext.Entry(sameCartItem).State = EntityState.Modified;
            }
            else
            {
                cart.Items.Add(new ECartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                });
                DbContext.Entry(cart).State = EntityState.Modified;
            }

            await DbContext.SaveChangesAsync();

            return cart;
        }

        public async Task<ECart> RemoveItemAsync(Guid cartId, Guid cartItemId)
        {
            var cart = await DbContext.Carts
                       .Include(c => c.Items)
                       .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart != null)
            {
                var removedItem = cart.Items.FirstOrDefault(x => x.Id == cartItemId);
                cart.Items.Remove(removedItem);

                DbContext.Entry(cart).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<ECart> ClearCartAsync(Guid cartId)
        {
            var cart = await DbContext.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart != null)
            {
                cart.Items.Clear();
                DbContext.Entry(cart).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }

            return cart;
        }
    }
}
