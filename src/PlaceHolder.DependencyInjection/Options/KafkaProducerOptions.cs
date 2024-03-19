namespace PlaceHolder.DependencyInjection.Options
{
    public class KafkaProducerOptions
    {
        public const string Position = "KafkaProducer";

        public string BootStrapServers { get; set; }
    }
}
