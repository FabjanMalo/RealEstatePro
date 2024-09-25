using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.Abstractions;
public record Error
{
    public static Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    public static Error NullValue = new("Error.NullValue", "The value provided is null", ErrorType.Failure);

    public static Error RelationError = new("Error.Relation", "This record has a relation with another entity so it cannot be deleted", ErrorType.Conflict);

    public static Error GeneralError = new("Error.Failure", "Something went wrong. Try again later!", ErrorType.Failure);

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }


    public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);
    public static Error BadRequest(string code, string description) => new(code, description, ErrorType.BadRequest);
    public static Error Conflict(string code, string description) => new Error(code, description, ErrorType.Conflict);
    public static Error Failure(string code, string description) => new Error(code, description, ErrorType.Failure);
    public static Error Unauthorized(string code, string description) => new Error(code, description, ErrorType.Unauthorized);
}
