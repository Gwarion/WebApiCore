using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlaceHolder.Application.Services.Ports.Kafka;
using PlaceHolder.DependencyInjection;
using PlaceHolder.DependencyInjection.Options;

namespace PlaceHolder.DrivenAdapter.KafkaProducer
{
    public class AdapterInstaller : IAdapterInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var kafkaOptions = new KafkaProducerOptions();
            configuration.GetSection(KafkaProducerOptions.Position).Bind(kafkaOptions);

            services.AddSingleton<IKafkaProducer, PlaceHolderKafkaProducer>(_ => new PlaceHolderKafkaProducer(kafkaOptions));
        }
    }
}
