using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Exceptions;
using PlaceHolder.Domain.SeedWork;
using System.Text.RegularExpressions;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.ValueObjects
{
    public class Email : ValueObject
    {        
        private const string MailPattern = @"^[\w\.\-_]+@[\w\-_]+.\w+$";

        private string _email;
        public override string Value => _email;
        
        public Email(string email)
        {
            if(!Regex.IsMatch(email, MailPattern))
            {
                throw new InvalidEmailFormatException(email);
            }
            _email = email;
        }

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string email) => new Email(email);
    }
}
