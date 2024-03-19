namespace PlaceHolder.Utils.Exceptions
{
    public static class ExceptionMessages
    {
        public const string InternalServerExceptionMessage = "Internal Server error";
        public const string NotImplementedExceptionMessage = "Feature is currently not supported";
        public const string NotFoundExceptionMessage = "Requested item was not found";
        public const string ValueObjectValidationExceptionMessage = "Invalid Request data";
        public const string InvalidConfigurationExceptionMessage= "Missing or invalid configuration";
        public const string KafkaProducerExceptionMessage = "Could not send Kafka message";
    }
}
