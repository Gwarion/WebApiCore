using PlaceHolder.Domain.SeedWork.Kafka;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Events
{
    [EventMetaData(topicName: "Consumer")]
    public class ConsumerUpdatedEvent : DomainEvent
    {
        public Consumer Consumer { get; set; }

        public ConsumerUpdatedEvent(Consumer consumer)
        {
            this.Guid = consumer.Guid;
            this.Consumer = consumer;
        }
    }
}
