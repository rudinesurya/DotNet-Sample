using DotNet_Sample.Controllers.Cart_Action;
using DotNet_Sample.Data;
using DotNet_Sample.Entity;
using DotNet_Sample.Test.Helper;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNet_Sample.Test.IntegrationTest
{
    public class CartTest : BaseIntegrationTest
    {
        static Func<AppDbContext, bool> seed = (db) =>
        {
            // Seed Carts
            db.Carts.AddRange(FixedData.GetFixedCarts());
            db.SaveChangesAsync();

            return true;
        };

        public CartTest() : base("Test", seed) { }

        [Fact]
        public async Task GetCartsAsync_ReturnCollection()
        {
            /// Act
            var result = await TestClient.GetAsync<List<Cart>>("/cart");

            /// Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(FixedData.GetFixedCarts().Count());
        }

        [Fact]
        public async Task GetCartByIdAsync_ReturnFound()
        {
            /// Arrange 
            var id = FixedData.GetFixedCarts().FirstOrDefault().Id;

            /// Act
            var result = await TestClient.GetAsync<Cart>($"/cart/{id}");

            /// Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetCartByIdAsync_ReturnNotFound()
        {
            /// Act
            var result = await TestClient.GetAsync<Cart>($"/cart/{Guid.NewGuid()}");

            /// Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddItemAsync_ReturnSuccess()
        {
            /// Arrange
            var productToAdd = FixedData.GetFixedProducts().FirstOrDefault();

            /// Act
            var result = await TestClient.PostAsyncAndReturn<AddCartItem, Cart>("/cart/additem", FixedData.GetNewAddCartItemAction("USER_NEW", productToAdd.Id));

            /// Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be("USER_NEW");
        }

        [Fact]
        public async Task RemoveItemAsync_ReturnSuccess()
        {
            /// Arrange
            var cartToRemove = FixedData.GetFixedCarts().FirstOrDefault();
            var cartItemToRemove = cartToRemove.Items.FirstOrDefault();

            /// Act
            var result = await TestClient.PostAsync<RemoveCartItem>("/cart/removeitem", FixedData.GetNewRemoveCartItemAction(cartToRemove.Id, cartItemToRemove.Id));

            /// Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ClearCartAsync_ReturnSuccess()
        {
            /// Arrange
            var cartToClear = FixedData.GetFixedCarts().FirstOrDefault();

            /// Act
            var result = await TestClient.PostAsync<Guid>("/cart/clearcart", cartToClear.Id);

            /// Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task CheckoutCartAsync_ReturnSuccess()
        {
            /// Arrange
            var cartToCheckout = FixedData.GetFixedCarts().FirstOrDefault();

            /// Act
            var result = await TestClient.PostAsyncAndReturn<Guid, Cart>("/cart/checkout", cartToCheckout.Id);

            /// Assert
            result.Should().NotBeNull();
        }
    }
}
