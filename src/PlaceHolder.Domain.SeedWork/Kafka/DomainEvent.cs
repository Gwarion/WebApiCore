using System;

namespace PlaceHolder.Domain.SeedWork.Kafka
{
    public abstract class DomainEvent
    {
        public Guid Guid { get; protected set; }
        public string MessageName => GetType().Name;
        public DateTime TimeStamp => DateTime.Now;
    }
}
