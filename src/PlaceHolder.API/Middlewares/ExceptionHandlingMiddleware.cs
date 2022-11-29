using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlaceHolder.Utils.Exceptions;
using PlaceHolder.Utils.Exceptions.DomainExceptions;
using PlaceHolder.Utils.Exceptions.TechnicalExceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PlaceHolder.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private static readonly string DefaultContentType = "application/json";
        private static readonly string DefaultLoggerCategoryName = "PlaceHolder";

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
            catch (Exception e)
            {
                _logger.LogError(message: e.Message);

                ErrorResponse errorResponse = e switch
                {
                    DomainException => await HandleDomainExceptionAsync(e),
                    TechnicalException => await HandleTechnicalExceptionAsync(e),
                    _ => await HandleExceptionAsync(e)
                };

                httpContext.Response.ContentType = DefaultContentType;
                httpContext.Response.StatusCode = (int)errorResponse.StatusCode;

                await httpContext.Response.WriteAsync(errorResponse.ToString());
            }
        }

        private static Task<ErrorResponse> HandleDomainExceptionAsync(Exception exception)
        {
            var errorResponse = new ErrorResponse { Success = false };

            switch (exception)
            {
                case NotFoundException _:
                    errorResponse.StatusCode = HttpStatusCode.NotFound;
                    errorResponse.Message = ExceptionMessages.NotFoundExceptionMessage;
                    break;

                default:
                    errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.Message = ExceptionMessages.InternalServerExceptionMessage;
                    break;
            }

            return Task.FromResult(errorResponse);
        }

        private static Task<ErrorResponse> HandleTechnicalExceptionAsync(Exception exception)
        {
            var errorResponse = new ErrorResponse { Success = false };

            switch (exception)
            {
                case ConfigurationException _:
                    errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.Message = ExceptionMessages.InvalidConfigurationExceptionMessage;
                    break;

                default:
                    errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.Message = ExceptionMessages.InternalServerExceptionMessage;
                    break;
            }

            return Task.FromResult(errorResponse);
        }

        private static Task<ErrorResponse> HandleExceptionAsync(Exception exception)
        {
            var errorResponse = new ErrorResponse { Success = false };

            switch (exception)
            {
                case NotImplementedException _:
                    errorResponse.StatusCode = HttpStatusCode.NotImplemented;
                    errorResponse.Message = ExceptionMessages.NotImplementedExceptionMessage;
                    break;
                case ValidationException _:
                    errorResponse.StatusCode = HttpStatusCode.BadRequest;
                    errorResponse.Message = $"{ExceptionMessages.ValueObjectValidationExceptionMessage} : {exception.Message}";
                    break;
                default:
                    errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.Message = ExceptionMessages.InternalServerExceptionMessage;
                    break;
            }

            return Task.FromResult(errorResponse);
        }
    }
}
