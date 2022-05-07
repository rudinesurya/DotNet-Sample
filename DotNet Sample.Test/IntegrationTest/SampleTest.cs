using DotNet_Sample.Test.Helper;
using DotNet_Sample.Test.MockData;
using FluentAssertions;
using System;
using System.Linq;
using System.Net.Http;
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
            var defaultCategory = await TestApiClient.AddCategoryAsync(Mapper.Map<SampleApiClient.Category>(FixedData.GetNewCategory(Guid.NewGuid(), "White")));
            var iPhoneX = await TestApiClient.AddProductAsync(Mapper.Map<SampleApiClient.Product>(FixedData.GetNewProduct(Guid.NewGuid(), "IPhone X", defaultCategory.Id)));

            // Add to cart
            await TestApiClient.AddCartItemAsync(Mapper.Map<SampleApiClient.AddCartItem>(FixedData.GetNewAddCartItemAction("Tester", iPhoneX.Id)));

            // checkout cart
            var cart = await TestApiClient.GetCartByUserNameAsync("Tester");
            await TestApiClient.CheckoutCartAsync(cart.Id);

            // Verify that the order gets created under the user name
            var result = await TestApiClient.GetOrdersByUserNameAsync("Tester");
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
            var defaultCategory = await TestApiClient.AddCategoryAsync(Mapper.Map<SampleApiClient.Category>(FixedData.GetNewCategory(Guid.NewGuid(), "White")));
            var iPhoneX = await TestApiClient.AddProductAsync(Mapper.Map<SampleApiClient.Product>(FixedData.GetNewProduct(Guid.NewGuid(), "IPhone X", defaultCategory.Id)));
            var s20 = await TestApiClient.AddProductAsync(Mapper.Map<SampleApiClient.Product>(FixedData.GetNewProduct(Guid.NewGuid(), "S20", defaultCategory.Id)));

            // Add to cart
            await TestApiClient.AddCartItemAsync(Mapper.Map<SampleApiClient.AddCartItem>(FixedData.GetNewAddCartItemAction("Tester", iPhoneX.Id)));
            await TestApiClient.AddCartItemAsync(Mapper.Map<SampleApiClient.AddCartItem>(FixedData.GetNewAddCartItemAction("Tester", s20.Id)));

            // Get the cart
            var cart = await TestApiClient.GetCartByUserNameAsync("Tester");

            // Change of decision. Remove iPhoneX
            await TestApiClient.RemoveCartItemAsync(Mapper.Map<SampleApiClient.RemoveCartItem>(FixedData.GetNewRemoveCartItemAction(cart.Id, cart.Items.ToList().Find(x => x.Product.Id == iPhoneX.Id).Id)));

            // Add another s20
            await TestApiClient.AddCartItemAsync(Mapper.Map<SampleApiClient.AddCartItem>(FixedData.GetNewAddCartItemAction("Tester", s20.Id)));

            // Verify that the cart contains only one product type with quantity=2
            cart = await TestApiClient.GetCartByUserNameAsync("Tester");
            cart.Items.Count.Should().Be(1);
            cart.Items.ToList()[0].Quantity.Should().Be(2);

            // checkout cart
            cart = await TestApiClient.GetCartByUserNameAsync("Tester");
            await TestApiClient.CheckoutCartAsync(cart.Id);

            // Verify that the order gets created under the user name
            var result = await TestApiClient.GetOrdersByUserNameAsync("Tester");
            result.Count.Should().Be(1);
        }
    }
}
