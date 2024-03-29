using BoDi;
using PlaceHolder.API.Controllers.Consumers.Resources;
using PlaceHolder.API.IntegrationTests.Utils;
using PlaceHolder.QueryModel.Consumers;

namespace PlaceHolder.API.IntegrationTests.StepDefinitions
{
    [Binding, Scope(Feature ="Consumers")]
    public class ConsumersSteps
    {
        private IObjectContainer _container;
        private AddressResource _addressResource;
        private ConsumerResource _consumerResource;

        public ConsumersSteps(IObjectContainer container) => _container = container;

        [Given(@"A Consumer with an address")]
        public void GivenAConsumerWithAnAddress()
        {
            _consumerResource = new()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@test.fr",
                PhoneNumber = "+33600000001",
                Address = new()
                {
                    Street = "1 rue du test",
                    PostalCode = 67000,
                    City = "TestCity",
                    Country = "TestCountry"
                }
            };

            _container.Resolve<HttpRequestTestTracker>().RegisterContent(_consumerResource);
        }

        [Then(@"I get the consumer")]
        public async Task ThenIGetTheConsumer()
        {
            var actual = await _container.Resolve<HttpRequestTestTracker>().DeserializeResponseAsync<ConsumerDto>();

            Assert.NotEqual(default, actual.Guid);
            Assert.Equal(_consumerResource.FirstName, actual.FirstName);
            Assert.Equal(_consumerResource.LastName, actual.LastName);
            Assert.Equal(_consumerResource.Email, actual.Email);
            Assert.Equal(_consumerResource.PhoneNumber, actual.PhoneNumber);

            var address = _consumerResource.Address;
            Assert.Equal($"{address.Street}, {address.PostalCode} {address.City} ({address.Country.ToUpper()})", actual.Address);
        }
    }
}
