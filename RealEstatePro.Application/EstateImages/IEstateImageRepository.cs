using RealEstatePro.Application.Abstractions.Contracts;
using RealEstatePro.Domain.EstateImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.EstateImages;
public interface IEstateImageRepository : IGenericRepository<EstateImage>
{
}
