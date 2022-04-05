using AutoMapper;
using DotNet_Sample.Controllers;
using DotNet_Sample.Controllers.Dto;
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
    public class ProductControllerTest : BaseControllerTest
    {
        [Fact]
        public async Task GetProductsAsync_ReturnCollection()
        {
            /// Arrange
            var service = new Mock<IProductService>();
            var productList = new List<EProduct>() { FixedData.GetNewEProduct(Guid.NewGuid(), "P1"), FixedData.GetNewEProduct(Guid.NewGuid(), "P2") };
            service.Setup(_ => _.GetProductsAsync()).ReturnsAsync(productList);
            var sut = new ProductController(service.Object, Mapper);

            /// Act
            var result = await sut.Get() as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Product>).Count.Should().Be(productList.Count());
        }

        [Fact]
        public async Task GetProductsAsync_ReturnEmptyCollection()
        {
            /// Arrange
            var service = new Mock<IProductService>();
            service.Setup(_ => _.GetProductsAsync()).ReturnsAsync(new List<EProduct>());
            var sut = new ProductController(service.Object, Mapper);

            /// Act
            var result = await sut.Get() as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Product>).Count.Should().Be(0);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnFound()
        {
            /// Arrange
            var service = new Mock<IProductService>();
            var productId = Guid.NewGuid();
            service.Setup(_ => _.GetProductByIdAsync(productId)).ReturnsAsync(FixedData.GetNewEProduct(productId, "P1"));
            var sut = new ProductController(service.Object, Mapper);

            /// Act
            var result = await sut.Get(productId) as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnNotFound()
        {
            /// Arrange
            var service = new Mock<IProductService>();
            var invalidId = Guid.NewGuid();
            service.Setup(_ => _.GetProductByIdAsync(invalidId)).ReturnsAsync(default(EProduct));
            var sut = new ProductController(service.Object, Mapper);

            /// Act
            var result = await sut.Get(invalidId) as NotFoundResult; 

            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task AddProductAsync_ReturnSuccess()
        {
            /// Arrange
            var service = new Mock<IProductService>();
            var newProductId = Guid.NewGuid();
            service.Setup(_ => _.AddProductAsync(FixedData.GetNewEProduct(newProductId, "PN"))).ReturnsAsync(FixedData.GetNewEProduct(newProductId, "PN"));
            var sut = new ProductController(service.Object, Mapper);

            /// Act
            var result = await sut.Add(FixedData.GetNewProduct(newProductId, "PN")) as CreatedAtActionResult;

            /// Assert
            result.StatusCode.Should().Be(201);
        }
    }
}