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
    public class OrderTest : BaseIntegrationTest
    {
        static Func<AppDbContext, bool> seed = (db) =>
        {
            // Seed Orders
            db.Orders.AddRange(FixedData.GetFixedOrders());

            db.SaveChangesAsync();

            return true;
        };

        public OrderTest() : base("Test", seed) { }

        [Fact]
        public async Task GetOrdersAsync_ReturnCollection()
        {
            /// Act
            var result = await TestClient.GetAsync<List<Order>>("/order");

            /// Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(FixedData.GetFixedOrders().Count());
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnFound()
        {
            /// Arrange 
            var id = FixedData.GetFixedOrders().FirstOrDefault().Id;

            /// Act
            var result = await TestClient.GetAsync<Product>($"/order/{id}");

            /// Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnNotFound()
        {
            /// Act
            var result = await TestClient.GetAsync<Product>($"/order/{Guid.NewGuid()}");

            /// Assert
            result.Should().BeNull();
        }
    }
}
