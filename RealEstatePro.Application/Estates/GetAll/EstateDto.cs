using RealEstatePro.Domain.EstateImages;
using RealEstatePro.Domain.Estates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.GetAll;
public record EstateDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public EstateCategory EstateCategory { get; init; }
    public string Address { get; init; }
    public decimal Price { get; init; }
    public decimal SurfaceArea { get; init; }

    public DateTime CreatedOnUtc { get; init; }


}
