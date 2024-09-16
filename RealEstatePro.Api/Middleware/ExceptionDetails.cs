using RealEstatePro.Domain.Abstractions;

namespace RealEstatePro.Api.Middleware;

public record ExceptionDetails(
    int Status,
    string Type,
    string Title,
    string Detail,
    IEnumerable<Error?> Errors);

