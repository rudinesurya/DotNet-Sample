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
    public class CartServiceTest : BaseServiceTest
    {
        List<ECart> seedList;
        Guid c1Id;

        public CartServiceTest()
        {
            if (DbContext.Database.EnsureCreated())
            {
                // Seed Products
                var c1 = FixedData.GetNewECart(Guid.NewGuid(), "U1");
                c1Id = c1.Id;
                var c2 = FixedData.GetNewECart(Guid.NewGuid(), "U2");

                seedList = new List<ECart>() { c1, c2 };

                DbContext.Carts.AddRange(seedList);
                DbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task GetCartsAsync_ReturnCollection()
        {
            /// Arrange
            var sut = new CartService(DbContext);

            /// Act
            var result = await sut.GetCartsAsync();

            /// Assert
            result.Should().HaveCount(seedList.Count());
        }

        [Fact]
        public async Task GetCartByIdAsync_ReturnFound()
        {
            /// Arrange
            var sut = new CartService(DbContext);

            /// Act
            var result = await sut.GetCartByIdAsync(c1Id);

            /// Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCartByIdAsync_ReturnNotFound()
        {
            /// Arrange
            var sut = new CartService(DbContext);

            /// Act
            var result = await sut.GetCartByIdAsync(Guid.Empty);

            /// Assert
            result.Should().BeNull();
        }
    }
}
