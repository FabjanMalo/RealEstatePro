using MediatR;
using RealEstatePro.Application.Estates.GetAll;
using RealEstatePro.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.GetPromoted;
public class GetPromotedEstatesQuery : IRequest<Result<List<EstateDto>>>
{ 
}
