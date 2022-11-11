using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlaceHolder.Application.Services.Ports.EF;
using PlaceHolder.DependencyInjection;
using System;

namespace PlaceHolder.API.Managers

{
    public static class DatabaseMigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

            if (Docker.IsStarted())
            {
                try
                {
                    dbContext.Migrate();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return host;
        }
    }
}
