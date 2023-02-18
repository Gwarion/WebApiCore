    using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.API.IntegrationTests.Utils
{
    public static class HttpClientHelper
    {
        private static HttpClient _httpClient { get; }

        public static readonly string BaseAddress = "http://localhost:80/api/v1";
        
        static HttpClientHelper() => _httpClient = new PlaceHolderWebApplicationFactory<Program>().CreateClient();

        public static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
            => await _httpClient.SendAsync(request);

        private class PlaceHolderWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : Program
        {
            protected override IHost CreateHost(IHostBuilder builder)
            {
                builder.UseEnvironment("Testing");
                return base.CreateHost(builder);
            }
        }
    }
}
