using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlaceHolder.Application.Services.Ports.EF;
using System;

namespace PlaceHolder.API
{
    public static class WebHostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

            try
            {
                dbContext.Migrate();
            }
            catch (Exception ex)
            {
                throw;
            }

            return host;
        }
    }
}
