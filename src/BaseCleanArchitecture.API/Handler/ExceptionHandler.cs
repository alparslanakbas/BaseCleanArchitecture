using CD.Results;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;


namespace BaseCleanArchitecture.API.Handler
{
    public sealed class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Result<string> errorResult;

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (exception.GetType() == typeof(ValidationException))
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

                errorResult = Result<string>.Failure(HttpStatusCode.Forbidden,((ValidationException)exception)
                    .Errors.
                        Select(x => x.PropertyName)
                        .ToList());

                await httpContext.Response.WriteAsJsonAsync(errorResult, cancellationToken: cancellationToken);
                return true;
            }

            errorResult = Result<string>.Failure(HttpStatusCode.InternalServerError, exception.Message);
            await httpContext.Response.WriteAsJsonAsync(errorResult, cancellationToken: cancellationToken);
            return true;
        }
    }
}
