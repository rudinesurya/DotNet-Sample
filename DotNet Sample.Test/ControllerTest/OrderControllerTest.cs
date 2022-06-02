using DotNet_Sample.Controllers;
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
    public class OrderControllerTest : BaseControllerTest
    {
        [Fact]
        public async Task GetOrdersAsync_ReturnCollection()
        {
            /// Arrange
            var service = new Mock<IOrderService>();
            var orderList = new List<Order>() { FixedData.GetNewOrder(Guid.NewGuid(), "USER_1"), FixedData.GetNewOrder(Guid.NewGuid(), "USER_2") };
            service.Setup(_ => _.GetOrdersAsync()).ReturnsAsync(orderList);
            var sut = new OrderController(service.Object);

            /// Act
            var result = await sut.Get() as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Order>).Count.Should().Be(orderList.Count());
        }

        [Fact]
        public async Task GetOrdersAsync_ReturnEmptyCollection()
        {
            /// Arrange
            var service = new Mock<IOrderService>();
            service.Setup(_ => _.GetOrdersAsync()).ReturnsAsync(new List<Order>());
            var sut = new OrderController(service.Object);

            /// Act
            var result = await sut.Get() as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Order>).Count.Should().Be(0);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnFound()
        {
            /// Arrange
            var service = new Mock<IOrderService>();
            var orderId = Guid.NewGuid();
            service.Setup(_ => _.GetOrderByIdAsync(orderId)).ReturnsAsync(FixedData.GetNewOrder(orderId, "USER_1"));
            var sut = new OrderController(service.Object);

            /// Act
            var result = await sut.Get(orderId) as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnNotFound()
        {
            /// Arrange
            var service = new Mock<IOrderService>();
            var invalidId = Guid.NewGuid();
            service.Setup(_ => _.GetOrderByIdAsync(invalidId)).ReturnsAsync(default(Order));
            var sut = new OrderController(service.Object);

            /// Act
            var result = await sut.Get(invalidId) as NotFoundResult;

            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
