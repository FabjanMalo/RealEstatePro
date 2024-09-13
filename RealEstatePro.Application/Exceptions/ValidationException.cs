using RealEstatePro.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Exceptions;
public class ValidationException(IEnumerable<Error> errors) : Exception
{
    public IEnumerable<Error> Errors { get; } = errors;
}
