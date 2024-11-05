using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using Ecommerce.Domain.src.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string responseMessage;

            switch (exception)
            {
                case UnauthorizedActionException:
                    statusCode = HttpStatusCode.Unauthorized; // 401 Unauthorized
                    responseMessage = FormatErrorMessage("Unauthorized access: credentials are not valid.");
                    break;

                case AuthenticationException authException:
                    statusCode = HttpStatusCode.Unauthorized; // 401 Unauthorized
                    responseMessage = FormatErrorMessage(authException.Message);
                    break;

                case EntityNotFoundException entityNotFoundException:
                    statusCode = HttpStatusCode.NotFound; // 404 Not Found
                    responseMessage = FormatErrorMessage(entityNotFoundException.Message);
                    break;

                case InvalidInputDataException invalidInputDataException:
                    statusCode = HttpStatusCode.BadRequest; // 400 Bad Request
                    responseMessage = FormatErrorMessage(invalidInputDataException.Message);
                    break;

                case ResourceConflictException resourceConflictException:
                    statusCode = HttpStatusCode.Conflict; // 409 Conflict
                    responseMessage = FormatErrorMessage(resourceConflictException.Message);
                    break;

                case InvalidQueryOptionException invalidQueryOptionException:
                    statusCode = HttpStatusCode.BadRequest; // 400 Bad Request
                    responseMessage = FormatErrorMessage(invalidQueryOptionException.Message);
                    break;

                case DuplicateEntityException duplicateEntityException:
                    statusCode = HttpStatusCode.Conflict; // 409 Conflict
                    responseMessage = FormatErrorMessage(duplicateEntityException.Message);
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError; // 500 Internal Server Error
                    responseMessage = FormatErrorMessage("An unexpected error occurred. Please try again later.");
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(responseMessage);
        }

        private static string FormatErrorMessage(string message)
        {
            return JsonSerializer.Serialize(new { error = message });
        }
    }
}