﻿using System.Net;
using System.Text.Json;

namespace PlaceHolder.API.Middlewares
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
