using DotNet_Sample.Controllers.Cart_Action;
using DotNet_Sample.Entity;
using System;

namespace DotNet_Sample.Test.MockData
{
    public class FixedData
    {
        #region DTO

        public static AddCartItem GetNewAddCartItemAction(string username, Guid productId)
        {
            return new AddCartItem()
            {
                UserName = username,
                ProductId = productId,
                Quantity = 1,
            };
        }

        public static RemoveCartItem GetNewRemoveCartItemAction(Guid cartId, Guid cartItemId)
        {
            return new RemoveCartItem()
            {
                CartId = cartId,
                CartItemId = cartItemId
            };
        }

        #endregion

        #region Entity

        public static Category GetNewCategory(Guid id, string name)
        {
            return new Category()
            {
                Id = id,
                Name = name,
                Description = "",
            };
        }

        public static Product GetNewProduct(Guid id, string name)
        {
            return new Product()
            {
                Id = id,
                Name = name,
                Summary = "Summary",
                Description = "Description",
                ImageFile = "default.png",
                Price = 1000.00M,
            };
        }

        public static Cart GetNewCart(Guid id, string username)
        {
            return new Cart()
            {
                Id = id,
                UserName = username,
            };
        }

        public static CartItem GetNewCartItem(Guid id, Guid productId)
        {
            return new CartItem()
            {
                Id = id,
                Quantity = 1,
                Price = 1,
                ProductId = productId,
            };
        }

        public static Order GetNewOrder(Guid id, string username)
        {
            return new Order()
            {
                Id = id,
                UserName = username,
                TotalPrice = 1000,
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
        }

        #endregion
    }
}
