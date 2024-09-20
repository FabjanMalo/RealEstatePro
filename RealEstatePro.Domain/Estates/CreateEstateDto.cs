using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RealEstatePro.Domain.Estates;
public record CreateEstateDto
{
    public Guid UserId { get; init; }
    public string Name { get; init; }
    public EstateCategory EstateCategory { get; init; }
    public string Address { get; init; }

    public decimal Price { get; init; }
    public string? Description { get; init; }
    public decimal SurfaceArea { get; init; }
    public int FloorNumber { get; init; }

    public bool IsPromoted { get; init; }

    public IList<IFormFile> Images { get; init; }

}
