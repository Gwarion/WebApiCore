using PlaceHolder.Application.Services.Ports.Kafka;
using PlaceHolder.Domain.SeedWork.Kafka;

namespace PlaceHolder.DrivenAdapter.KafkaProducer.Mock
{
    internal class PlaceHolderKafkaProducerMock : IKafkaProducer
    {
        public async Task ProduceAsync(DomainEvent domainEvent)
        {
            await Task.CompletedTask;
        }
    }
}
