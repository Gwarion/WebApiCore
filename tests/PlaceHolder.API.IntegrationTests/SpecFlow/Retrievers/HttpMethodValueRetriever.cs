using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace PlaceHolder.API.IntegrationTests.SpecFlow.Retrievers
{
    public class HttpMethodValueRetriever : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) 
            => keyValuePair.Value != null && propertyType == typeof(HttpMethod);

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return keyValuePair.Value.ToUpper() switch
            {
                "DELETE" => HttpMethod.Delete,
                "GET" => HttpMethod.Get,
                "PATCH" => HttpMethod.Patch,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                _ => throw new NotImplementedException()
            };
        }
    }
}
