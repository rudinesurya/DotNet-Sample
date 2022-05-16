using DotNet_Sample.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DotNet_Sample.Test.Helper
{
    public static class ExtensionMethods
    {
        public static async Task<V?> GetAsync<V>(this HttpClient client, string requestUri)
        {
            return await (await client.GetAsync(requestUri)).Content.ReadAsAsync<V>();
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T payload)
        {
            return await client.PostAsync(requestUri, JsonContent.Create(payload));
        }

        public static async Task<V?> PostAsyncAndReturn<T, V>(this HttpClient client, string requestUri, T payload)
        {
            return await (await client.PostAsync(requestUri, JsonContent.Create(payload))).Content.ReadAsAsync<V>();
        }
    }

    public abstract class BaseIntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;

        public BaseIntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                        if (descriptor != null)
                            services.Remove(descriptor);

                        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "InMemoryDbForTesting"));
                    });
                });

            TestClient = appFactory.CreateClient();
        }

        public void Dispose()
        {
            TestClient.Dispose();
        }
    }
}
