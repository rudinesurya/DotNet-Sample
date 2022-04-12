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
        Guid p1Id;

        public ProductServiceTest()
        {
            if (DbContext.Database.EnsureCreated())
            {
                // Seed Products
                var p1 = FixedData.GetNewEProduct(Guid.NewGuid(), "PRODUCT_1");
                p1Id = p1.Id;
                p1.Category = FixedData.GetNewECategory(Guid.NewGuid(), "CAT_1");
                var p2 = FixedData.GetNewEProduct(Guid.NewGuid(), "PRODUCT_2");
                p2.Category = FixedData.GetNewECategory(Guid.NewGuid(), "CAT_2");

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
            var result = await sut.GetProductByIdAsync(Guid.Empty);

            /// Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddProductAsync_ReturnSuccess()
        {
            /// Arrange
            var sut = new ProductService(DbContext);
            var p = FixedData.GetNewEProduct(Guid.NewGuid(), "PRODUCT_NEW");
            p.Category = FixedData.GetNewECategory(Guid.NewGuid(), "CAT_NEW");

            /// Act
            var result = await sut.AddProductAsync(p);

            /// Assert
            result.Should().NotBeNull();
        }
    }
}
