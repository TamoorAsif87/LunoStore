using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shared.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError($"Error Message: {exception.Message}, Time occurred on ${DateTime.UtcNow}");

        (string Details, string Name, int StatusCode) details = exception switch
        {
            InternalServerException =>
            (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
            ),
            NotFoundException =>
           (
               exception.Message,
               exception.GetType().Name,
               httpContext.Response.StatusCode = StatusCodes.Status404NotFound

           ),
           ApplicationException => 
           (
               exception.Message,
               exception.GetType().Name,
               httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
           ),

           ArgumentException => 
           (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
           ),

           UnauthorizedAccessException =>
           (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized
           ),

            BadRequestException =>
            (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            ),

            ValidationException =>
          (
              exception.Message,
              exception.GetType().Name,
              httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
          ),
          _ => 
          (
              exception.Message,
              exception.GetType().Name,
              httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
          )
        };

        var problemDetails = new ProblemDetails
        {
            Detail = details.Details,
            Status = details.StatusCode,
            Title = details.Name
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if(exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationsErrors", validationException.Errors.Select(err => new { key =
                err.PropertyName, errorMessage = err.ErrorMessage }));
        }

        await httpContext.Response.WriteAsJsonAsync( problemDetails, cancellationToken);

        return true;
    }
}
