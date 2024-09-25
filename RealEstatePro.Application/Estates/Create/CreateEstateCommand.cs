using MediatR;
using RealEstatePro.Domain.Abstractions;
using RealEstatePro.Domain.Estates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.Create;
public class CreateEstateCommand : IRequest<Result<Guid>>
{
    public CreateEstateDto CreateEstateDto { get; set; }
}
