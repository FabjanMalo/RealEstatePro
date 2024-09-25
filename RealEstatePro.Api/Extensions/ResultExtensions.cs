using Azure.Core;
using RealEstatePro.Domain.Abstractions;

namespace RealEstatePro.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Can't convert success result to problem");
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error.Type),
            type: GetType(result.Error.Type),
            extensions: new Dictionary<string, object?>
            {
                {"errors",  new[]{ result.Error} }
            });
    }
    private static int GetStatusCode(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
    }
    private static string GetTitle(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.BadRequest => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            ErrorType.Unauthorized => "Unauthorized",
            _ => "Server Failure"
        };
    }
    private static string GetType(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.BadRequest => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            ErrorType.Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };
    }
}
