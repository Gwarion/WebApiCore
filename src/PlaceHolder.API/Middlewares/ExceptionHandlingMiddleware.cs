using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlaceHolder.Domain.SeedWork.Exceptions.Base;
using PlaceHolder.Utils;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PlaceHolder.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private const string DefaultContentType = "application/json";
        private const string DefaultLoggerCategoryName = "PlaceHolder";

        private static readonly string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger(DefaultLoggerCategoryName);
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BaseException be)
            {
                await HandleExceptionAsync(httpContext, be.StatusCode, be.Message, be.StackTrace);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "An unmanaged exception occured.", e.StackTrace);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context, 
            HttpStatusCode statusCode,
            string message, 
            string stackTrace)
        {
            _logger.LogError(message);

            ErrorResponse errorResponse = new(statusCode);

            if (_environment.In(Environments.Development, Environments.Staging))
            {
                errorResponse.EnrichMessage(message);
                errorResponse.EnrichStackTrace(stackTrace);
            }

            await context.Response.WriteAsync(errorResponse.ToString());
        }
    }
}
