using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Data;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNet_Sample.Test.ServiceTest
{
    public class ProductServiceTest : IDisposable
    {
        private readonly AppDbContext DbContext;

        public ProductServiceTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            DbContext = new AppDbContext(options);
        }

        [Fact]
        public async Task GetProductsAsync_ReturnCollection()
        {
            /// Arrange
            var sut = new ProductService(DbContext);

            /// Act
            var result = await sut.GetProductsAsync();

            /// Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetProductsAsync_ReturnCollection2()
        {
            /// Arrange
            var sut = new ProductService(DbContext);

            /// Act
            var result = await sut.GetProductsAsync();

            /// Assert
            result.Should().HaveCount(2);
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }
}
