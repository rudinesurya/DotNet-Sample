using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using DotNet_Sample.Test.Helper;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNet_Sample.Test.ServiceTest
{
    public class CategoryServiceTest : BaseServiceTest
    {
        List<Category> seedList;
        Guid c1Id;

        public CategoryServiceTest()
        {
            if (DbContext.Database.EnsureCreated())
            {
                // Seed Products
                var c1 = FixedData.GetNewCategory(Guid.NewGuid(), "CAT_1");
                c1Id = c1.Id;
                var c2 = FixedData.GetNewCategory(Guid.NewGuid(), "CAT_2");

                seedList = new List<Category>() { c1, c2 };

                DbContext.Categories.AddRange(seedList);
                DbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task GetCategoriesAsync_ReturnCollection()
        {
            /// Arrange
            var sut = new CategoryService(DbContext);

            /// Act
            var result = await sut.GetCategoriesAsync().ToListAsync();

            /// Assert
            result.Should().HaveCount(seedList.Count());
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnFound()
        {
            /// Arrange
            var sut = new CategoryService(DbContext);

            /// Act
            var result = await sut.GetCategoryByIdAsync(c1Id);

            /// Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnNotFound()
        {
            /// Arrange
            var sut = new CategoryService(DbContext);

            /// Act
            var result = await sut.GetCategoryByIdAsync(Guid.Empty);

            /// Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddCategoryAsync_ReturnSuccess()
        {
            /// Arrange
            var sut = new CategoryService(DbContext);
            var c = FixedData.GetNewCategory(Guid.NewGuid(), "CAT_NEW");

            /// Act
            var result = await sut.AddCategoryAsync(c);

            /// Assert
            result.Should().NotBeNull();
        }
    }
}
