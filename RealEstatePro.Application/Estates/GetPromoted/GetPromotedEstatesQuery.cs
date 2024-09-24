using MediatR;
using RealEstatePro.Application.Estates.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.GetPromoted;
public class GetPromotedEstatesQuery : IRequest<List<EstateDto>>
{
}
