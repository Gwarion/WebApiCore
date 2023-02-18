using PlaceHolder.Application.Services.Ports.Kafka;
using PlaceHolder.Domain.SeedWork.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
