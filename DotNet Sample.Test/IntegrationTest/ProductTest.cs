using DotNet_Sample.Entity;
using DotNet_Sample.Test.Helper;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DotNet_Sample.Test.IntegrationTest
{
    public class ProductTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetProductsAsync_ReturnCollection()
        {
            /// Arrange
            await TestClient.PostAsyncAndReturn<Product, Product>("/product", FixedData.GetNewProduct(Guid.NewGuid(), "PRODUCT_1"));
            await TestClient.PostAsyncAndReturn<Product, Product>("/product", FixedData.GetNewProduct(Guid.NewGuid(), "PRODUCT_2"));

            /// Act
            var result = await TestClient.GetAsync<List<Product>>("/product");

            /// Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }

        [Fact]
        public async Task GetProductsAsync_ReturnEmptyCollection()
        {
            /// Arrange

            /// Act
            var result = await TestClient.GetAsync<List<Product>>("/product");

            /// Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnFound()
        {
            /// Arrange
            var p = await TestClient.PostAsyncAndReturn<Product, Product>("/product", FixedData.GetNewProduct(Guid.NewGuid(), "PRODUCT_1"));

            /// Act
            var result = await TestClient.GetAsync<Product>($"/product/{p.Id}");

            /// Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(p.Id);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnNotFound()
        {
            /// Arrange

            /// Act
            var result = await TestClient.GetAsync<Product>($"/product/{Guid.NewGuid()}");

            /// Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddProductAsync_ReturnSuccess()
        {
            /// Arrange
            var p = await TestClient.PostAsyncAndReturn<Product, Product>("/product", FixedData.GetNewProduct(Guid.NewGuid(), "PRODUCT_NEW"));

            /// Act
            var result = await TestClient.GetAsync<Product>($"/product/{p.Id}");

            /// Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(p.Id);
        }
    }
}
