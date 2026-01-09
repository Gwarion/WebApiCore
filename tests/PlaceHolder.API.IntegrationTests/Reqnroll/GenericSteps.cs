using PlaceHolder.API.IntegrationTests.Utils;
using System.Net;

namespace PlaceHolder.API.IntegrationTests.SpecFlow
{
    [Binding]
    public class GenericSteps
    {
        private readonly IObjectContainer _container;
        private readonly HttpRequestTestTracker _tracker;

        public GenericSteps(IObjectContainer container, HttpRequestTestTracker tracker)
        {
            _container = container;
            _tracker = tracker;
        }

        [When(@"I send the following request")]
        public async Task WhenISendTheFollowingRequest(Table table)
        {
            try
            {
                var request = table.CreateInstance<HttpRequestMessage>();
                request.Content = _tracker.Content;

                var response = await HttpClientHelper.SendAsync(request);
                _tracker.RegisterResponse(response);
            }
            catch(Exception e)
            {
                _tracker.RegisterException(e);
            }
        }

        [Then(@"No exception occurs")]
        public void ThenNoExceptionOccurs()
        {
            Assert.False(_tracker.ExceptionOccured);
        }

        [Then(@"I get the status code '([^']*)'")]
        public void ThenIGetTheStatusCode(HttpStatusCode expetedStatusCode)
        {
            _tracker.AssertResponse(expetedStatusCode);
        }
    }
}
