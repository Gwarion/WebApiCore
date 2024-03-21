using PlaceHolder.Domain.Model.Shared.Exceptions;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Exceptions
{
    public class InvalidEmailFormatException : DomainException
    {
        public InvalidEmailFormatException(string value)
         : base($"Unexpected email format : {value}") { }
    }
}
