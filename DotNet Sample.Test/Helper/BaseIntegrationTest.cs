using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Controllers.Dto.Cart_Action;
using DotNet_Sample.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;

namespace DotNet_Sample.Test.Helper
{
    public abstract class BaseIntegrationTest : IDisposable
    {
        protected readonly Mapper Mapper;
        protected readonly HttpClient TestHttpClient;
        protected readonly SampleApiClient.SampleApiClient TestApiClient;

        public BaseIntegrationTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, SampleApiClient.Product>();
                cfg.CreateMap<Category, SampleApiClient.Category>();
                cfg.CreateMap<AddCartItem, SampleApiClient.AddCartItem>();
                cfg.CreateMap<RemoveCartItem, SampleApiClient.RemoveCartItem>();
            });

            Mapper = new Mapper(config);

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

            TestHttpClient = appFactory.CreateClient();
            TestApiClient = new SampleApiClient.SampleApiClient(null, TestHttpClient);
        }

        public void Dispose()
        {
            TestHttpClient.Dispose();
        }
    }
}
