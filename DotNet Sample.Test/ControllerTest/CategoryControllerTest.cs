﻿using DotNet_Sample.Controllers;
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
    public class CategoryControllerTest : BaseControllerTest
    {
        [Fact]
        public async Task GetCategoriesAsync_ReturnCollection()
        {
            /// Arrange
            var service = new Mock<ICategoryService>();
            var categoryList = new List<ECategory>() { FixedData.GetNewECategory(Guid.NewGuid(), "C1"), FixedData.GetNewECategory(Guid.NewGuid(), "C2") };
            service.Setup(_ => _.GetCategoriesAsync()).ReturnsAsync(categoryList);
            var sut = new CategoryController(service.Object, Mapper);

            /// Act
            var result = await sut.Get() as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Category>).Count.Should().Be(categoryList.Count());
        }

        [Fact]
        public async Task GetCategoriesAsync_ReturnEmptyCollection()
        {
            /// Arrange
            var service = new Mock<ICategoryService>();
            service.Setup(_ => _.GetCategoriesAsync()).ReturnsAsync(new List<ECategory>());
            var sut = new CategoryController(service.Object, Mapper);

            /// Act
            var result = await sut.Get() as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
            (result.Value as List<Category>).Count.Should().Be(0);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnFound()
        {
            /// Arrange
            var service = new Mock<ICategoryService>();
            var categoryId = Guid.NewGuid();
            service.Setup(_ => _.GetCategoryByIdAsync(categoryId)).ReturnsAsync(FixedData.GetNewECategory(categoryId, "C1"));
            var sut = new CategoryController(service.Object, Mapper);

            /// Act
            var result = await sut.Get(categoryId) as OkObjectResult;

            /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnNotFound()
        {
            /// Arrange
            var service = new Mock<ICategoryService>();
            var invalidId = Guid.NewGuid();
            service.Setup(_ => _.GetCategoryByIdAsync(invalidId)).ReturnsAsync(default(ECategory));
            var sut = new CategoryController(service.Object, Mapper);

            /// Act
            var result = await sut.Get(invalidId) as NotFoundResult;

            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task AddCategoryAsync_ReturnSuccess()
        {
            /// Arrange
            var service = new Mock<ICategoryService>();
            var newCategoryId = Guid.NewGuid();
            service.Setup(_ => _.AddCategoryAsync(FixedData.GetNewECategory(newCategoryId, "CN"))).ReturnsAsync(FixedData.GetNewECategory(newCategoryId, "CN"));
            var sut = new CategoryController(service.Object, Mapper);

            /// Act
            var result = await sut.Add(FixedData.GetNewCategory(newCategoryId, "CN")) as CreatedAtActionResult;

            /// Assert
            result.StatusCode.Should().Be(201);
        }
    }
}