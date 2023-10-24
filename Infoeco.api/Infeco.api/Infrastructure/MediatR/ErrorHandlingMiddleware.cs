using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace Infeco.Api.Infrastructure.MediatR
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // If needed, log issues
            HttpStatusCode code;
            string result;

            if (exception.GetType() == typeof(ValidationException))
            {
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new { validationError = ((ValidationException)exception).Errors.Select(_ => _.ErrorMessage) });
            }
            else
            {
                code = HttpStatusCode.InternalServerError;
#if DEBUG 
                result = JsonConvert.SerializeObject(new { error = $"{exception.InnerException} {exception.Message}", stackTrace = exception.StackTrace });
#else
                result = JsonConvert.SerializeObject(new { error = exception.Message });
#endif
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
