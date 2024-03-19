using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlaceHolder.Application.Services.Ports.EF;

namespace PlaceHolder.API.Managers

{
    public static class DatabaseMigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
            dbContext.Migrate();

            return host;
        }
    }
}
