using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Exceptions;
using PlaceHolder.Domain.SeedWork;
using System.Text.RegularExpressions;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private const string PhoneNumberPattern = @"^(0|\+\d{2})\d{9}$";

        private string _phoneNumber;
        public override string Value => _phoneNumber;

        public PhoneNumber(string phoneNumber)
        {
            if (!Regex.IsMatch(phoneNumber, PhoneNumberPattern))
            {
                throw new InvalidPhoneNumberFormatException(phoneNumber);
            }

            _phoneNumber = phoneNumber;
        }

        public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
        public static implicit operator PhoneNumber(string phoneNumber) => new PhoneNumber(phoneNumber);
    }
}
