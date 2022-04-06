using DotNet_Sample.Controllers;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using DotNet_Sample.Test.Helper;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNet_Sample.Test.ControllerTest
{
    public class CartControllerTest : BaseControllerTest
    {
        [Fact]
        public async Task GetCartsAsync_ReturnCollection()
        {
            /// Arrange
            var service = new Mock<ICartService>();
            var cartList = new List<ECart>() { FixedData.GetNewECart(Guid.NewGuid(), "C1"), FixedData.GetNewECart(Guid.NewGuid(), "C2") };
            service.Setup(_ => _.GetCartsAsync()).ReturnsAsync(cartList);
            var sut = new CartController(service.Object, Mapper);

            /// Act
            var result = await sut.Get() as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Cart>).Count.Should().Be(cartList.Count());
        }

        [Fact]
        public async Task GetOrdersAsync_ReturnEmptyCollection()
        {
            /// Arrange
            var service = new Mock<ICartService>();
            service.Setup(_ => _.GetCartsAsync()).ReturnsAsync(new List<ECart>());
            var sut = new CartController(service.Object, Mapper);

            /// Act
            var result = await sut.Get() as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Cart>).Count.Should().Be(0);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnFound()
        {
            /// Arrange
            var service = new Mock<ICartService>();
            var cartId = Guid.NewGuid();
            service.Setup(_ => _.GetCartByIdAsync(cartId)).ReturnsAsync(FixedData.GetNewECart(cartId, "C1"));
            var sut = new CartController(service.Object, Mapper);

            /// Act
            var result = await sut.Get(cartId) as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnNotFound()
        {
            /// Arrange
            var service = new Mock<ICartService>();
            var invalidId = Guid.NewGuid();
            service.Setup(_ => _.GetCartByIdAsync(invalidId)).ReturnsAsync(default(ECart));
            var sut = new CartController(service.Object, Mapper);

            /// Act
            var result = await sut.Get(invalidId) as NotFoundResult;

            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task AddItemAsync_ReturnSuccess()
        {
            /// Arrange
            var service = new Mock<ICartService>();

            var productId = Guid.NewGuid();
            var cart = FixedData.GetNewECart(Guid.NewGuid(), "U");
            cart.Items = new List<ECartItem> { FixedData.GetNewECartItem(Guid.NewGuid(), productId) };
            
            service.Setup(_ => _.AddItemAsync("U", productId, 1)).ReturnsAsync(cart);
            var sut = new CartController(service.Object, Mapper);

            /// Act
            var result = await sut.AddItem(FixedData.GetNewAddCartItemAction("U", productId)) as CreatedAtActionResult;

            /// Assert
            result.StatusCode.Should().Be(201);
        }

        public async Task RemoveItemAsync_ReturnSuccess()
        {
            /// Arrange
            var service = new Mock<ICartService>();

            var cartId = Guid.NewGuid();
            var cartItemId = Guid.NewGuid();

            service.Setup(_ => _.RemoveItemAsync(cartId, cartItemId)).ReturnsAsync(FixedData.GetNewECart(cartId, "C1"));
            var sut = new CartController(service.Object, Mapper);

            /// Act
            var result = await sut.RemoveItem(FixedData.GetNewRemoveCartItemAction(cartId, cartItemId)) as NoContentResult;

            /// Assert
            result.StatusCode.Should().Be(204);
        }

        public async Task ClearCartAsync_ReturnSuccess()
        {
            /// Arrange
            var service = new Mock<ICartService>();

            var cartId = Guid.NewGuid();

            service.Setup(_ => _.ClearCartAsync(cartId)).ReturnsAsync(FixedData.GetNewECart(cartId, "C1"));
            var sut = new CartController(service.Object, Mapper);

            /// Act
            var result = await sut.ClearCart(cartId) as NoContentResult;

            /// Assert
            result.StatusCode.Should().Be(204);
        }
    }
}
