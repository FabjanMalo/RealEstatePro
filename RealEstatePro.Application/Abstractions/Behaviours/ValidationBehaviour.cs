using FluentValidation;
using MediatR;
using RealEstatePro.Application.Exceptions;
using RealEstatePro.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error = RealEstatePro.Domain.Abstractions.Error;
using ValidationException = RealEstatePro.Application.Exceptions.ValidationException;

namespace RealEstatePro.Application.Abstractions.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {

        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(v => v.Validate(context))
            .Where(v => v.Errors.Count != 0)
            .SelectMany(v => v.Errors)
            .Select(v => Error.BadRequest(v.PropertyName, v.ErrorMessage))
            .ToList();

        if (validationErrors.Count != 0)
        {
            throw new ValidationException(validationErrors);
        }
        else
        {
            return await next();
        }

    }
}
