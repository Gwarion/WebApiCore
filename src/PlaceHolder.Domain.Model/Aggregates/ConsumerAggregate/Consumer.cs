using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.ValueObjects;
using PlaceHolder.Domain.SeedWork;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate
{
    public sealed class Consumer : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
    }
}
