using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using DotNet_Sample.Test.Helper;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
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

        public OrderServiceTest()
        {
            if (DbContext.Database.EnsureCreated())
            {
                // Seed Products
                var o1 = FixedData.GetNewEOrder(Guid.NewGuid(), "U1");
                o1Id = o1.Id;
                var o2 = FixedData.GetNewEOrder(Guid.NewGuid(), "U2");

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
    }
}
