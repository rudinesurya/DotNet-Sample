﻿using DotNet_Sample.Controllers.Service;
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
    public class CartServiceTest : BaseServiceTest
    {
        List<Cart> seedList;
        Guid c1Id;
        Cart c2;

        public CartServiceTest()
        {
            if (DbContext.Database.EnsureCreated())
            {
                // Seed Products
                var c1 = FixedData.GetNewCart(Guid.NewGuid(), "USER_1");
                c1Id = c1.Id;

                c2 = FixedData.GetNewCart(Guid.NewGuid(), "USER_2");
                c2.Items = new List<CartItem> { FixedData.GetNewCartItem(Guid.NewGuid(), Guid.NewGuid()) };

                seedList = new List<Cart>() { c1, c2 };

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
            var result = await sut.GetCartsAsync().ToListAsync();

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

        [Fact]
        public async Task AddItemAsync_ReturnSuccess()
        {
            /// Arrange
            var sut = new CartService(DbContext);

            /// Act
            var result = await sut.AddItemAsync("USER_1", Guid.NewGuid(), 1);

            /// Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task AddItemAsyncWithNewUser_ReturnSuccess()
        {
            /// Arrange
            var sut = new CartService(DbContext);

            /// Act
            var result = await sut.AddItemAsync("USER_NEW", Guid.NewGuid(), 1);

            /// Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task AddItemAsyncToAppendToCurrentCart_ReturnSuccess()
        {
            /// Arrange
            var sut = new CartService(DbContext);

            /// Act
            var result = await sut.AddItemAsync("USER_2", Guid.NewGuid(), 1);

            /// Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task RemoveItemAsync_ReturnSuccess()
        {
            /// Arrange
            var sut = new CartService(DbContext);

            /// Act
            var result = await sut.RemoveItemAsync(c2.Id, c2.Items.First().Id);

            /// Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(0);
        }

        [Fact]
        public async Task ClearCartAsync_ReturnSuccess()
        {
            /// Arrange
            var sut = new CartService(DbContext);

            /// Act
            var result = await sut.ClearCartAsync(c2.Id);

            /// Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(0);
        }
    }
}
