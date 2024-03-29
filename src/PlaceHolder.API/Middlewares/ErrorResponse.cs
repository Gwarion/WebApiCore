using System.Net;
using System.Text.Json;

namespace PlaceHolder.API.Middlewares
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; private set; } = "An error occured";
        public bool Success { get; private set; } = false;
        public string StackTrace { get; private set; } = null;

        public ErrorResponse(HttpStatusCode statusCode)
            => StatusCode = statusCode;

        public void EnrichMessage(string message)
            => Message = message;

        public void EnrichStackTrace(string stackTrace)
            => StackTrace = stackTrace;

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
