using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Test.Helper;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DotNet_Sample.Test.IntegrationTest
{
    public class SampleTest : BaseIntegrationTest
    {
        [Fact]
        public async Task TestForSuccessfullOrderCreation()
        {
            /// User creates product
            /// Add product to cart and checkout
            /// Verify that the order gets created under the user name


            // Create product
            var iPhoneX = await TestClient.PostAsyncAndReturn<Product, Product>("/product", FixedData.GetNewProduct(Guid.NewGuid(), "IPhone X"));

            // Add to cart
            await TestClient.PostAsync("/cart/additem", FixedData.GetNewAddCartItemAction("Tester", iPhoneX.Id));

            // checkout cart
            var cart = await TestClient.GetAsync<Cart>("/cart/username/Tester");
            await TestClient.PostAsync("/cart/checkout", cart.Id);

            // Verify that the order gets created under the user name
            var result = await TestClient.GetAsync<List<Order>>("/order/username/Tester");
            result.Count.Should().Be(1);
        }

        [Fact]
        public async Task StressTest1()
        {
            /// User creates 2 products
            /// Add 2 product to cart and checkout
            /// remove one product and add another to make quantity=2
            /// Verify that the cart contains only one product type with quantity=2
            /// Verify that the order gets created under the user name


            // Create product
            var iPhoneX = await TestClient.PostAsyncAndReturn<Product, Product>("/product", FixedData.GetNewProduct(Guid.NewGuid(), "IPhone X"));
            var s20 = await TestClient.PostAsyncAndReturn<Product, Product>("/product", FixedData.GetNewProduct(Guid.NewGuid(), "S20"));

            // Add to cart
            await TestClient.PostAsync("/cart/additem", FixedData.GetNewAddCartItemAction("Tester", iPhoneX.Id));
            await TestClient.PostAsync("/cart/additem", FixedData.GetNewAddCartItemAction("Tester", s20.Id));

            // Get the cart
            var cart = await TestClient.GetAsync<Cart>("/cart/username/Tester");

            // Change of decision. Remove iPhoneX
            await TestClient.PostAsync("/cart/removeitem", FixedData.GetNewRemoveCartItemAction(cart.Id, cart.Items.Find(x => x.Product.Id == iPhoneX.Id).Id));

            // Add another s20
            await TestClient.PostAsync("/cart/additem", FixedData.GetNewAddCartItemAction("Tester", s20.Id));

            // Verify that the cart contains only one product type with quantity=2
            cart = await TestClient.GetAsync<Cart>("/cart/username/Tester");
            cart.Items.Count.Should().Be(1);
            cart.Items[0].Quantity.Should().Be(2);

            // checkout cart
            await TestClient.PostAsync("/cart/checkout", cart.Id);

            // Verify that the order gets created under the user name
            var result = await TestClient.GetAsync<List<Order>>("/order/username/Tester");
            result.Count.Should().Be(1);
        }
    }
}
