using Confluent.Kafka;
using Newtonsoft.Json;
using PlaceHolder.Application.Services.Ports.Kafka;
using PlaceHolder.DependencyInjection.Options;
using PlaceHolder.Domain.SeedWork.Kafka;
using PlaceHolder.Utils.Exceptions.DomainExceptions;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PlaceHolder.DrivenAdapter.KafkaProducer
{
    public class PlaceHolderKafkaProducer : IKafkaProducer
    {
        private readonly KafkaProducerOptions _options;
        private readonly ProducerConfig _producerConfig;

        public PlaceHolderKafkaProducer(KafkaProducerOptions options)
        {
            _options = options;

            _producerConfig = new ProducerConfig
            {
                BootstrapServers = _options.BootStrapServers
            };
        }

        public async Task ProduceAsync(DomainEvent domainEvent)
        {
            if (domainEvent.GetType().GetCustomAttributes().SingleOrDefault(a => a is EventMetaData) is not EventMetaData metaData)
            {
                throw new KafkaProducerException($"Metadata not set for {domainEvent.GetType().Name}");
            }

            using var p = new ProducerBuilder<string, string>(_producerConfig).Build();

            await p.ProduceAsync(metaData.TopicName, new Message<string, string>
            {
                Key = domainEvent.Guid.ToString(),
                Value = JsonConvert.SerializeObject(domainEvent),
            });
        }
    }
}
