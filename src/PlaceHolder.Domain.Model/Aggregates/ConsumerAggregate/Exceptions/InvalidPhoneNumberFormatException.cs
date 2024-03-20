using PlaceHolder.Domain.Model.Shared.Exceptions;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Exceptions
{
    public class InvalidPhoneNumberFormatException : DomainException
    {
        public InvalidPhoneNumberFormatException(string value)
        : base($"Unexpected phone number format : {value}") { }
    }
}
