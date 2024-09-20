using MediatR;
using RealEstatePro.Domain.Estates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.Create;
public class CreateEstateCommand : IRequest<Guid>
{
    public CreateEstateDto CreateEstateDto { get; set; }
}
