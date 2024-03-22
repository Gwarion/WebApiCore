using BoDi;
using PlaceHolder.API.Controllers.Consumers.Resources;
using PlaceHolder.API.IntegrationTests.Utils;
using PlaceHolder.QueryModel.Consumers;
using TechTalk.SpecFlow.Assist;

namespace PlaceHolder.API.IntegrationTests.StepDefinitions
{
    [Binding, Scope(Feature ="Consumers")]
    public class ConsumersSteps
    {
        private IObjectContainer _container;
        private AddressResource _addressResource;
        private ConsumerResource _consumerResource;

        public ConsumersSteps(IObjectContainer container) => _container = container;

        [Given(@"the following Address")]
        public void GivenTheFollowingAddress(Table table)
        {
            _addressResource = table.CreateInstance<AddressResource>();
        }

        [Given(@"the following Consumer")]
        public void GivenTheFollowingConsumer(Table table)
        {
            _consumerResource = table.CreateInstance<ConsumerResource>();
            _consumerResource.Address = _addressResource;

            _container.Resolve<HttpRequestTestTracker>().RegisterContent(_consumerResource);
        }

        [Then(@"I Get the following Consumer")]
        public async Task ThenIGetTheFollowingConsumer(Table table)
        {
            var expected = table.CreateInstance<ConsumerDto>();
            var actual = await _container.Resolve<HttpRequestTestTracker>().DeserializeResponseAsync<ConsumerDto>();

            Assert.NotEqual(default, actual.Guid);
            Assert.Equal(expected.FirstName, expected.FirstName);
            Assert.Equal(expected.LastName, expected.LastName);
        }
    }
}
