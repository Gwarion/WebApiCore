using System;

namespace PlaceHolder.Domain.SeedWork.Kafka
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventMetaData : Attribute
    {
        public string TopicName { get; }

        public EventMetaData(string topicName)
        {
            if (string.IsNullOrWhiteSpace(topicName)) throw new ArgumentNullException(nameof(topicName));

            TopicName = topicName;
        }
    }
}
