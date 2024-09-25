using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.Abstractions;
public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public static Result Success() => new Result(true, Error.None);
    public static Result Failure(Error error) => new(false, error);


    public static Result<T> Success<T>(T value)
    {
        return new Result<T>(value, true, Error.None);
    }

    public static Result<T> Failure<T>(Error error)
    {
        return new Result<T>(default, false, error);
    }

    protected static Result<T> Create<T>(T? value)
    {
        return value is not null ? Success(value) : Failure<T>(Error.NullValue);
    }

}

public class Result<T> : Result
{
    private readonly T? _value;
    protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("The value of a failure result cannot be accessed!");

    public static implicit operator Result<T>(T? value) => Create(value);
}
