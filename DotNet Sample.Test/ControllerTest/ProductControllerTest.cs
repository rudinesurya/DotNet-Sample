using AutoMapper;
using DotNet_Sample.Controllers;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Controllers.Mapper;
using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DotNet_Sample.Test.ControllerTest
{
    public class ProductControllerTest
    {
        private readonly IMapper Mapper;

        public ProductControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(CartMapperProfile));
                cfg.AddProfile(typeof(CategoryMapperProfile));
                cfg.AddProfile(typeof(OrderMapperProfile));
                cfg.AddProfile(typeof(ProductMapperProfile));
            });

            Mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task GetProductsAsync_ReturnCollection()
        {
            /// Arrange
            var service = new Mock<IProductService>();
            service.Setup(_ => _.GetProductsAsync()).ReturnsAsync(FixedData.GetFixedProducts());
            var sut = new ProductController(service.Object, Mapper);

            /// Act
            var result = await sut.Get() as OkObjectResult;

            // /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Product>).Count.Should().Be(2);
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

            // /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Product>).Count.Should().Be(0);
        }
    }
}