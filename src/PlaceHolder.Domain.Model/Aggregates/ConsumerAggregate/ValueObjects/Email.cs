using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Exceptions;
using PlaceHolder.Domain.SeedWork;
using System.Text.RegularExpressions;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.ValueObjects
{
    public partial class Email : ValueObject
    {
        [GeneratedRegex(@"^[\w\.\-_]+@[\w\-_]+.\w+$")]
        private static partial Regex MailRegex();

        private readonly string _email;

        public override string Value => _email;
        
        public Email(string email)
        {
            if(!MailRegex().IsMatch(email))
            {
                throw new InvalidEmailFormatException(email);
            }
            _email = email;
        }

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string email) => new(email);


    }
}
