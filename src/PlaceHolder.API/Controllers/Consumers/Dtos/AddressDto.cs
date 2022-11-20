using Newtonsoft.Json;

namespace PlaceHolder.API.Controllers.Consumers.Dtos
{
    public class AddressDto
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("postalCode")]
        public int PostalCode { get; set; }
    }
}
