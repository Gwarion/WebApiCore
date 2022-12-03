using PlaceHolder.Domain.SeedWork.Kafka;
using System;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Events
{
    [EventMetaData(topicName:"Consumer")]
    public class ConsumerCreatedEvent : DomainEvent
    {
        public Consumer Consumer { get; set; }

        public ConsumerCreatedEvent(Consumer consumer)
        {
            this.Guid = consumer.Guid;
            this.Consumer = consumer;
        }
    }
}
