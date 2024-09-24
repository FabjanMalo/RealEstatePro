using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.GetAll;
public class GetAllEstateQueryHandler
    (IApplicationContext _context,
    IMapper _mapper)
    : IRequestHandler<GetAllEstateQuery, List<EstateDto>>
{
    public async Task<List<EstateDto>> Handle(GetAllEstateQuery request, CancellationToken cancellationToken)
    {

        var estate = await _context.Estates
            .ProjectTo<EstateDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return estate;
    }
}
