using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Application.Estates.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.GetPromoted;
public class GetPromotedEstatesQueryHandler
    (IApplicationContext _context)
    : IRequestHandler<GetPromotedEstatesQuery, List<EstateDto>>
{
    public async Task<List<EstateDto>> Handle(GetPromotedEstatesQuery request, CancellationToken cancellationToken)
    {
        var estate = await _context.Estates
              .Where(e => e.IsPromoted == true)
              .Select(e => new EstateDto
              {
                  Id = e.Id,
                  Name = e.Name,
                  EstateCategory = e.EstateCategory,
                  Address = e.Address,
                  Price = e.Price,
                  SurfaceArea = e.SurfaceArea,
                  CreatedOnUtc = e.CreatedOnUtc
              })
              .OrderByDescending(e => e.CreatedOnUtc)
              .ToListAsync(cancellationToken);

        return estate;
    }
}
