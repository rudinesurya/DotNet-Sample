using DotNet_Sample.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;

namespace DotNet_Sample.Test.Helper
{
    public abstract class BaseServiceTest : IDisposable
    {
        protected readonly AppDbContext DbContext;

        public BaseServiceTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            DbContext = new AppDbContext(options);
        }

        public void Dispose()
        {
            DbContext.ChangeTracker.Clear();
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }
}
