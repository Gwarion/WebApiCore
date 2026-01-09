using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlaceHolder.Application.Services.Ports.EF;
using PlaceHolder.DependencyInjection;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts;
using PlaceHolder.DrivenAdapter.SQLServer.QueryRepositories;
using PlaceHolder.DrivenAdapter.SQLServer.Repositories;
using PlaceHolder.QueryModel.Consumers;

namespace PlaceHolder.DrivenAdapter.SQLServer.Mock
{
    public class AdapterInstaller : IAdapterInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<PlaceHolderContext>
            (
                options => options.UseInMemoryDatabase("TestDB")
            );
            services.AddScoped<IDbContext, PlaceHolderContext>();

            services.AddAutoMapper(cfg => cfg.LicenseKey = "", GetType().Assembly);

            services.TryAddTransient<IConsumerRepository, ConsumerRepository>();

            services.TryAddTransient<IConsumerQueryRepository, ConsumerQueryRepository>();
        }
    }
}
