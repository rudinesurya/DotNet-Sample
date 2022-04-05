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
    public class ProductServiceTest : BaseServiceTest
    {
        List<EProduct> seedList;
        Guid p1Id = Guid.NewGuid();

        public ProductServiceTest()
        {
            if (DbContext.Database.EnsureCreated())
            {
                // Seed Products
                var p1 = FixedData.GetNewEProduct(p1Id, "P1"); 
                p1.Category = FixedData.GetNewECategory(Guid.NewGuid(), "C1");
                var p2 = FixedData.GetNewEProduct(Guid.NewGuid(), "P2");
                p2.Category = FixedData.GetNewECategory(Guid.NewGuid(), "C2");

                seedList = new List<EProduct>() { p1, p2 };

                DbContext.Products.AddRange(seedList);
                DbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task GetProductsAsync_ReturnCollection()
        {
            /// Arrange
            var sut = new ProductService(DbContext);

            /// Act
            var result = await sut.GetProductsAsync();

            /// Assert
            result.Should().HaveCount(seedList.Count());
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnFound()
        {
            /// Arrange
            var sut = new ProductService(DbContext);

            /// Act
            var result = await sut.GetProductByIdAsync(p1Id);

            /// Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnNotFound()
        {
            /// Arrange
            var sut = new ProductService(DbContext);

            /// Act
            var result = await sut.GetProductByIdAsync();

            /// Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddProductAsync_ReturnSuccess()
        {
            /// Arrange
            var sut = new ProductService(DbContext);

            /// Act
            var result = await sut.AddProductAsync(FixedData.GetNewEProduct(Guid.NewGuid()));

            /// Assert
            result.Should().NotBeNull();
        }
    }
}
