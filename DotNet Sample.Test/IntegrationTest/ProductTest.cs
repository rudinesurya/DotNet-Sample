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
    public class ProductTest : BaseIntegrationTest
    {
        static Func<AppDbContext, bool> seed = (db) =>
        {
            // Seed Products
            db.Products.AddRange(FixedData.GetFixedProducts());

            db.SaveChangesAsync();

            return true;
        };

        public ProductTest() : base("Test", seed) { }

        [Fact]
        public async Task GetProductsAsync_ReturnCollection()
        {
            /// Act
            var result = await TestClient.GetAsync<List<Product>>("/product");

            /// Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(FixedData.GetFixedProducts().Count());
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnFound()
        {
            /// Arrange 
            var id = FixedData.GetFixedProducts().FirstOrDefault().Id;

            /// Act
            var result = await TestClient.GetAsync<Product>($"/product/{id}");

            /// Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnNotFound()
        {
            /// Act
            var result = await TestClient.GetAsync<Product>($"/product/{Guid.NewGuid()}");

            /// Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddProductAsync_ReturnSuccess()
        {
            /// Arrange
            var newProduct = await TestClient.PostAsyncAndReturn<Product, Product>("/product", FixedData.GetNewProduct(Guid.NewGuid(), "PRODUCT_NEW"));

            /// Act
            var result = await TestClient.GetAsync<Product>($"/product/{newProduct.Id}");

            /// Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(newProduct.Id);
        }
    }
}
