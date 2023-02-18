using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.API.IntegrationTests.Utils
{
    public class HttpRequestTestTracker
    {
        public StringContent Content { get; private set; }

        private HttpResponseMessage _response;
        private List<Exception> _exceptions = new();

        public bool ExceptionOccured => _exceptions.Any();

        public void RegisterResponse(HttpResponseMessage response) => _response = response;
        public void RegisterException(Exception e) => _exceptions.Add(e);
        public void SetContent<TData>(TData data)
         => Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        public void AssertResponse(HttpStatusCode expectedStatusCode)
        {
            Assert.NotNull(_response);
            Assert.Equal(expectedStatusCode, _response.StatusCode);
        }

        public async Task AssertData<TExpected>(TExpected expected)
        {
            var body = await _response.Content.ReadAsStringAsync();
            Assert.NotNull(body);
            Assert.NotEmpty(body);

            var actual = JsonConvert.DeserializeObject<TExpected>(body);
            Assert.NotNull(actual);

            AssertObject(expected, actual);
        }

        private void AssertObject(object expected, object actual)
        {
            var type = expected.GetType();
            foreach (var property in type.GetProperties().Where(p => p.DeclaringType == type))
            {
                var propertyInfo = type.GetProperty(property.Name);
                var expectedData = propertyInfo.GetValue(expected);

                if (expectedData == null) { continue; }

                var actualData = propertyInfo.GetValue(actual);

                if (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(string))
                {
                    Assert.Equal(expectedData, actualData);
                }
                else
                {
                    AssertObject(expectedData, actualData);
                }
            }
        }
    }
}
