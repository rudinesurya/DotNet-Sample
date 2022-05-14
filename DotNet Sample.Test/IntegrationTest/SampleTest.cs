using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Test.Helper;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace DotNet_Sample.Test.IntegrationTest
{
    public class SampleTest : BaseIntegrationTest
    {
        [Fact]
        public async Task TestForSuccessfullOrderCreation()
        {
            HttpResponseMessage response;

            /// User creates product
            /// Add product to cart and checkout
            /// Verify that the order gets created under the user name

            // Create product
            var iPhoneX = await (await TestClient.PostAsync("/product",
                JsonContent.Create(FixedData.GetNewProduct(Guid.NewGuid(), "IPhone X"))
                )).Content.ReadAsAsync<Product>();

            // Add to cart
            await TestClient.PostAsync("/cart/additem", JsonContent.Create(FixedData.GetNewAddCartItemAction("Tester", iPhoneX.Id)));

            // checkout cart
            var cart = await (await TestClient.GetAsync("/cart/username/Tester")).Content.ReadAsAsync<Cart>();
            await TestClient.PostAsync("/cart/checkout", JsonContent.Create(cart.Id));

            // Verify that the order gets created under the user name
            var result = await (await TestClient.GetAsync("/order/username/Tester")).Content.ReadAsAsync<List<Order>>();
            result.Count.Should().Be(1);
        }

        [Fact]
        public async Task StressTest1()
        {
            HttpResponseMessage response;

            /// User creates 2 products
            /// Add 2 product to cart and checkout
            /// remove one product and add another to make quantity=2
            /// Verify that the cart contains only one product type with quantity=2
            /// Verify that the order gets created under the user name


            // Create product
            var iPhoneX = await (await TestClient.PostAsync("/product",
                JsonContent.Create(FixedData.GetNewProduct(Guid.NewGuid(), "IPhone X"))
                )).Content.ReadAsAsync<Product>();
            var s20 = await (await TestClient.PostAsync("/product",
                JsonContent.Create(FixedData.GetNewProduct(Guid.NewGuid(), "S20"))
                )).Content.ReadAsAsync<Product>();

            // Add to cart
            await TestClient.PostAsync("/cart/additem", JsonContent.Create(FixedData.GetNewAddCartItemAction("Tester", iPhoneX.Id)));
            await TestClient.PostAsync("/cart/additem", JsonContent.Create(FixedData.GetNewAddCartItemAction("Tester", s20.Id)));

            // Get the cart
            var cart = await (await TestClient.GetAsync("/cart/username/Tester")).Content.ReadAsAsync<Cart>();

            // Change of decision. Remove iPhoneX
            await TestClient.PostAsync("/cart/removeitem", JsonContent.Create(FixedData.GetNewRemoveCartItemAction(cart.Id, cart.Items.Find(x => x.Product.Id == iPhoneX.Id).Id)));

            // Add another s20
            await TestClient.PostAsync("/cart/additem", JsonContent.Create(FixedData.GetNewAddCartItemAction("Tester", s20.Id)));

            // Verify that the cart contains only one product type with quantity=2
            cart = await (await TestClient.GetAsync("/cart/username/Tester")).Content.ReadAsAsync<Cart>();
            cart.Items.Count.Should().Be(1);
            cart.Items[0].Quantity.Should().Be(2);

            // checkout cart
            await TestClient.PostAsync("/cart/checkout", JsonContent.Create(cart.Id));

            // Verify that the order gets created under the user name
            var result = await (await TestClient.GetAsync("/order/username/Tester")).Content.ReadAsAsync<List<Order>>();
            result.Count.Should().Be(1);
        }
    }
}
