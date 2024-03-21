using TechTalk.SpecFlow.Assist;

namespace PlaceHolder.API.IntegrationTests.SpecFlow.Retrievers
{
    public class UriValueRetriever : IValueRetriever
    {
        private string _baseUri;
        public UriValueRetriever(string baseUri) => _baseUri = baseUri;

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) 
            => keyValuePair.Value != null && propertyType == typeof(Uri);

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) 
            => new Uri($"{_baseUri}/{keyValuePair.Value}");
    }
}
