using PlaceHolder.Domain.SeedWork.Kafka;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Ports.Kafka
{
    public interface IKafkaProducer
    {
        public Task ProduceAsync(DomainEvent domainEvent);
    }
}
