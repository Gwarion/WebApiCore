using PlaceHolder.API.IntegrationTests.SpecFlow.Retrievers;
using PlaceHolder.API.IntegrationTests.Utils;

namespace PlaceHolder.API.IntegrationTests.Hooks
{
    [Binding]
    public static class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun(IObjectContainer container)
        {
            Service.Instance.ValueRetrievers.Register(new HttpMethodValueRetriever());
            Service.Instance.ValueRetrievers.Register(new UriValueRetriever(HttpClientHelper.BaseAddress));
        }

        [BeforeScenario]
        public static void BeforeScenario(IObjectContainer container)
        {
            container.RegisterInstanceAs<HttpRequestTestTracker>(new HttpRequestTestTracker());
        }
    }
}
