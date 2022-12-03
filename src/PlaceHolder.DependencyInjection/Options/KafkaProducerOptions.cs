namespace PlaceHolder.DependencyInjection.Options
{
    public class KafkaProducerOptions
    {
        public static readonly string Position = "KafkaProducer";

        public string BootStrapServers { get; set; }
    }
}
