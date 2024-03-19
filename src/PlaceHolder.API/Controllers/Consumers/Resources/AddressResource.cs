using Newtonsoft.Json;

namespace PlaceHolder.API.Controllers.Consumers.Resources
{
    public class AddressResource
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
