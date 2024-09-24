using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.GetAll;
public class GetAllEstateQuery : IRequest<List<EstateDto>>
{
}
