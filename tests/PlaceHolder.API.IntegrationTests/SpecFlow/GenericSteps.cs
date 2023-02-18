using BoDi;
using PlaceHolder.API.IntegrationTests.Utils;
using System.Net;
using TechTalk.SpecFlow.Assist;

namespace PlaceHolder.API.IntegrationTests.SpecFlow
{
    [Binding]
    public class GenericSteps
    {
        private IObjectContainer _container;
        public GenericSteps(IObjectContainer container) => _container = container;

        [When(@"I send the following request")]
        public async Task WhenISendTheFollowingRequest(Table table)
        {
            var tracker = _container.Resolve<HttpRequestTestTracker>();
            try
            {
                var request = table.CreateInstance<HttpRequestMessage>();
                request.Content = tracker.Content;

                var response = await HttpClientHelper.SendAsync(request);
                tracker.RegisterResponse(response);
            }
            catch(Exception e)
            {
                tracker.RegisterException(e);
            }
        }

        [Then(@"No exception occurs")]
        public void ThenNoExceptionOccurs()
        {
            Assert.False(_container.Resolve<HttpRequestTestTracker>().ExceptionOccured);
        }

        [Then(@"I Get the status code '([^']*)'")]
        public void ThenIGetTheStatusCode(HttpStatusCode expetedStatusCode)
        {
            _container.Resolve<HttpRequestTestTracker>().AssertResponse(expetedStatusCode);
        }
    }
}
