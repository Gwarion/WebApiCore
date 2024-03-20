namespace PlaceHolder.Utils.Exceptions.TechnicalExceptions
{
    public class KafkaProducerException : TechnicalException
    {
        public KafkaProducerException(string message) : base(message) { }
    }
}
