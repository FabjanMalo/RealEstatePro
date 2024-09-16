using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RealEstatePro.Application.Exceptions;

namespace RealEstatePro.Api.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, $"An exception happened, {exception.Message}");

        var exceptionDetails = GetExceptionDetails(exception);

        var problemDetails = new ProblemDetails
        {
            Status = exceptionDetails.Status,
            Type = exceptionDetails.Type,
            Title = exceptionDetails.Title,
            Detail = exceptionDetails.Detail

        };

        if (exceptionDetails is not null)
        {
            problemDetails.Extensions["Errors"] = exceptionDetails.Errors;
        }

        httpContext.Response.StatusCode = exceptionDetails.Status;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    public static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {

            ValidationException validationException => new ExceptionDetails(
               StatusCodes.Status400BadRequest,
               "Validation Failure",
               "Validation Error",
               "One or more validation errors has occurred",
               validationException.Errors
                ),

            ConcurrencyException concurrencyException => new ExceptionDetails(
                StatusCodes.Status409Conflict,
                "Concurrency Failure",
                "Concurrency Error",
                "Try again later",
                null
                ),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "Server Failure",
                "Server Error",
                "An unexpected error has happened",
                null
                )
        };
    }
}
