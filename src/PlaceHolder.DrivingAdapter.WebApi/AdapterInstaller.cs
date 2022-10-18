using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlaceHolder.API.Controllers.Consumers;
using PlaceHolder.DependencyInjection;
using PlaceHolder.DrivingAdapter.WebApi.Consumers;

namespace PlaceHolder.DrivingAdapter.WebApi
{
    public class AdapterInstaller : IAdapterInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(GetType().Assembly);
            services.TryAddTransient<IConsumerService, ConsumerService>();
        }
    }
}
