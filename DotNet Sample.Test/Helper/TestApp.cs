using DotNet_Sample.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DotNet_Sample.Test.Helper
{
    public class TestApp : WebApplicationFactory<Program>
    {
        private readonly string _environment;

        public TestApp(string environment = "Test")
        {
            _environment = environment;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            var databaseName = "testdb" + Guid.NewGuid().ToString();

            // Add mock/test services to the builder here
            builder.ConfigureServices(services =>
            {
                services.AddScoped(sp =>
                {
                    return new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName)
                        .UseApplicationServiceProvider(sp)
                        .Options;
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();
                    TestAppDbContextSeed.Seed(db);
                }
            });

            return base.CreateHost(builder);
        }
    }
}
