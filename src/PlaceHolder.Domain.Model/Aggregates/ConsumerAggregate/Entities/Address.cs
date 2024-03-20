using PlaceHolder.Domain.SeedWork;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate
{
    public class Address : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int PostalCode { get; set; }
    }
}
