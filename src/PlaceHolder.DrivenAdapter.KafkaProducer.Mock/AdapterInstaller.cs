using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlaceHolder.Application.Services.Ports.Kafka;
using PlaceHolder.DependencyInjection;

namespace PlaceHolder.DrivenAdapter.KafkaProducer.Mock
{
    public class AdapterInstaller : IAdapterInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IKafkaProducer, PlaceHolderKafkaProducerMock>();
        }
    }
}
