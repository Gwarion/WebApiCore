namespace PlaceHolder.Utils.Exceptions.DomainExceptions
{
    public class KafkaProducerException : DomainException
    {
        public KafkaProducerException(string message) : base(message) { }
    }
}
