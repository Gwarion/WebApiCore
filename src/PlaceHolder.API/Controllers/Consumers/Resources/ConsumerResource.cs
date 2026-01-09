namespace PlaceHolder.API.Controllers.Consumers.Resources
{
    public class ConsumerResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public AddressResource Address { get; set; }
    }
}
