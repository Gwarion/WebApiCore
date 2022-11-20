using Newtonsoft.Json;

namespace PlaceHolder.API.Controllers.Consumers.Dtos
{
    [JsonObject("consumer")]
    public class ConsumerDto
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("creationDate")]
        public string CreationDate { get; set; }

        [JsonProperty("modificationDate")]
        public string ModificationDate { get; set; }

        [JsonProperty("address")]
        public AddressDto Address { get; set; }
    }
}
