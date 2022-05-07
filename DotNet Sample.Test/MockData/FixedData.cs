using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Controllers.Dto.Cart_Action;
using DotNet_Sample.Entity;
using System;

namespace DotNet_Sample.Test.MockData
{
    public class FixedData
    {
        #region DTO

        public static Category GetNewCategory(Guid id, string name)
        {
            return new Category()
            {
                Id = id,
                Name = name,
                Description = "",
            };
        }

        public static Product GetNewProduct(Guid id, string name, Guid categoryId = default)
        {
            return new Product()
            {
                Id = id,
                CategoryId = categoryId,
                Name = name,
                Summary = "Summary",
                Description = "Description",
                ImageFile = "default.png",
                Price = 1000.00M,
            };
        }

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

        public static ECategory GetNewECategory(Guid id, string name)
        {
            return new ECategory()
            {
                Id = id,
                Name = name,
                Description = "",
            };
        }

        public static EProduct GetNewEProduct(Guid id, string name)
        {
            return new EProduct()
            {
                Id = id,
                Name = name,
                Summary = "Summary",
                Description = "Description",
                ImageFile = "default.png",
                Price = 1000.00M,
            };
        }

        public static ECart GetNewECart(Guid id, string username)
        {
            return new ECart()
            {
                Id = id,
                UserName = username,
            };
        }

        public static ECartItem GetNewECartItem(Guid id, Guid productId)
        {
            return new ECartItem()
            {
                Id = id,
                Quantity = 1,
                Price = 1,
                ProductId = productId,
            };
        }

        public static EOrder GetNewEOrder(Guid id, string username)
        {
            return new EOrder()
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
