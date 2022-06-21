using DotNet_Sample.Controllers.Cart_Action;
using DotNet_Sample.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<Category> GetFixedCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = Guid.Parse("64a7388c-65e9-42e1-bab1-39cd105f8675"),
                    Name = "White",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                },
                new Category()
                {
                    Id = Guid.Parse("c921ef76-0af6-4ac9-a7fd-864b4898d60b"),
                    Name = "Black",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                },
            };
        }

        public static IEnumerable<Product> GetFixedProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = Guid.Parse("2cff423d-0852-4406-ac3a-32a39a0253c0"),
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = GetFixedCategories().FirstOrDefault(c => c.Name == "White"),
                },
                new Product()
                {
                    Id = Guid.Parse("c8e86dc0-17ab-4b0e-8205-1c29afe50c09"),
                    Name = "Samsung 10",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = GetFixedCategories().FirstOrDefault(c => c.Name == "Black"),
                },
            };
        }

        public static IEnumerable<Order> GetFixedOrders()
        {
            return new List<Order>()
            {
                new Order()
                {
                    Id = Guid.Parse("449e01fc-4c6e-43ce-994b-75cafe457832"),
                    CartId = GetFixedCarts().FirstOrDefault().Id,
                    UserName = GetFixedCarts().FirstOrDefault().UserName,
                    TotalPrice = GetFixedCarts().FirstOrDefault().TotalPrice,
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
                },
            };
        }

        public static IEnumerable<Cart> GetFixedCarts()
        {
            return new List<Cart>()
            {
                new Cart()
                {
                    Id = Guid.Parse("91c83cb4-b835-48d8-bfae-7d5faf535ac4"),
                    UserName = "User 1",
                    Items = new List<CartItem>()
                    {
                        new CartItem()
                        {
                            Id = Guid.Parse("7047ab31-d79e-4e16-a06b-4c267be1c9e3"),
                            Quantity = 1,
                            Price = 1000,
                            Product = GetFixedProducts().FirstOrDefault(p => p.Name == "IPhone X"),
                        }
                    },
                },
            };
        }
    }
}
