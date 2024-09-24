using MediatR;
using Microsoft.AspNetCore.Http;
using RealEstatePro.Domain.EstateImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.AddImages;
public class AddEstateImageCommand : IRequest
{
    public Guid EstateId { get; set; }
    public List<IFormFile> Images { get; set; }
}
