using Newtonsoft.Json;
using System;

namespace PlaceHolder.API.Controllers.Consumers.Resources
{
    [JsonObject("consumer")]
    public class ConsumerResource
    {
        [JsonProperty("guid")]
        public Guid? Guid { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("creationDate")]
        public string CreationDate { get; set; }

        [JsonProperty("modificationDate")]
        public string ModificationDate { get; set; }

        [JsonProperty("address")]
        public AddressResource Address { get; set; }
    }
}
