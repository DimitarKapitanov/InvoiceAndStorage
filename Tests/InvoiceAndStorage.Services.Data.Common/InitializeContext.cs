using System;
using InvoiceAndStorage.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAndStorage.Services.Data.Common
{
    public static class InitializeContext
    {
        public static ApplicationDbContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var context = new ApplicationDbContext(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }
    }
}
