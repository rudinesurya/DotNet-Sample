﻿using DotNet_Sample.Data;
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