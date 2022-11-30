using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlaceHolder.Application.Services.Ports.EF;
using PlaceHolder.DependencyInjection;
using PlaceHolder.DependencyInjection.Options;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts;
using PlaceHolder.DrivenAdapter.SQLServer.Repositories;
using System;

namespace PlaceHolder.DrivenAdapter.SQLServer
{
    public class AdapterInstaller : IAdapterInstaller
    {
        private static readonly int _maxRetryCount = 4;
        private static readonly TimeSpan _maxRetryDelay = new(0, 0, 5);

        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var dbOptions = new DatabaseOptions();
            configuration.GetSection(DatabaseOptions.Position).Bind(dbOptions);

            services.AddDbContextPool<PlaceHolderContext>((provider, options) =>
            {
                //cf https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                options.UseSqlServer(
                    dbOptions.GetConnectionString(),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: _maxRetryCount,
                            maxRetryDelay: _maxRetryDelay,
                            errorNumbersToAdd: null);
                    });
            });

            services.AddAutoMapper(GetType().Assembly);

            services.TryAddTransient<IConsumerRepository, ConsumerRepository>();
            services.AddScoped<IDbContext, PlaceHolderContext>();
        }
    }
}
