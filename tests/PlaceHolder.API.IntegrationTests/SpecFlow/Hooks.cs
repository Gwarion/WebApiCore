using BoDi;
using PlaceHolder.API.IntegrationTests.SpecFlow.Retrievers;
using PlaceHolder.API.IntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

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
            container.RegisterInstanceAs<TestResultTracker>(new TestResultTracker());
        }
    }
}
