using BoDi;
using PlaceHolder.API.Controllers.Consumers.Dtos;
using PlaceHolder.API.IntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace PlaceHolder.API.IntegrationTests.SpecFlow
{
    [Binding]
    public class GenericSteps
    {
        private IObjectContainer _container;
        public GenericSteps(IObjectContainer container) => _container = container;

        [When(@"I send the following request")]
        public async void WhenISendTheFollowingRequest(HttpRequestMessage request)
        {
            var tracker = _container.Resolve<TestResultTracker>();
            try
            {
                tracker.RegisterResponse(await HttpClientHelper.SendAsync(request));
            }
            catch(Exception e)
            {
                tracker.RegisterException(e);
            }
        }

        [Then(@"No exception occurs")]
        public void ThenNoExceptionOccurs()
        {
            Assert.False(_container.Resolve<TestResultTracker>().ExceptionOccured);
        }

        [Then(@"I Get the status code '([^']*)'")]
        public void ThenIGetTheStatusCode(HttpStatusCode expetedStatusCode)
        {
            _container.Resolve<TestResultTracker>().AssertResponse(expetedStatusCode);
        }
    }
}
