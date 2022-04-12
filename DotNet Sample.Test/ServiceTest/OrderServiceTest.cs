using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using DotNet_Sample.Test.Helper;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNet_Sample.Test.ServiceTest
{
    public class OrderServiceTest : BaseServiceTest
    {
        List<EOrder> seedList;
        Guid o1Id;
        Guid o2CartId;

        public OrderServiceTest()
        {
            if (DbContext.Database.EnsureCreated())
            {
                // Seed Products
                var o1 = FixedData.GetNewEOrder(Guid.NewGuid(), "USER_1");
                o1Id = o1.Id;
                var o2 = FixedData.GetNewEOrder(Guid.NewGuid(), "USER_2");
                o2CartId = Guid.NewGuid();
                o2.CartId = o2CartId;

                seedList = new List<EOrder>() { o1, o2 };

                DbContext.Orders.AddRange(seedList);
                DbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task GetOrdersAsync_ReturnCollection()
        {
            /// Arrange
            var sut = new OrderService(DbContext);

            /// Act
            var result = await sut.GetOrdersAsync();

            /// Assert
            result.Should().HaveCount(seedList.Count());
        }

        [Fact]
        public async Task GetOrdersByUserNameAsync_ReturnCollection()
        {
            /// Arrange
            var sut = new OrderService(DbContext);

            /// Act
            var result = await sut.GetOrdersByUserNameAsync("USER_1");

            /// Assert
            result.Should().HaveCount(seedList.Where(o => o.UserName == "USER_1").Count());
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnFound()
        {
            /// Arrange
            var sut = new OrderService(DbContext);

            /// Act
            var result = await sut.GetOrderByIdAsync(o1Id);

            /// Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnNotFound()
        {
            /// Arrange
            var sut = new OrderService(DbContext);

            /// Act
            var result = await sut.GetOrderByIdAsync(Guid.Empty);

            /// Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetOrderByCartIdAsync_ReturnFound()
        {
            /// Arrange
            var sut = new OrderService(DbContext);
            
            /// Act
            var result = await sut.GetOrderByCartIdAsync(o2CartId);

            /// Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task AddOrderAsync_ReturnSuccess()
        {
            /// Arrange
            var sut = new OrderService(DbContext);
            var o = FixedData.GetNewEOrder(Guid.NewGuid(), "USER_1");

            /// Act
            var result = await sut.AddOrderAsync(o);

            /// Assert
            result.Should().NotBeNull();
        }
    }
}
