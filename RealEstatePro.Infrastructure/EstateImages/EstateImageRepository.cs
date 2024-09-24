using RealEstatePro.Application.EstateImages;
using RealEstatePro.Domain.EstateImages;
using RealEstatePro.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure.EstateImages;
public class EstateImageRepository : GenericRepository<EstateImage>, IEstateImageRepository
{
    private readonly RealEstateDbContext _context;

    public EstateImageRepository(RealEstateDbContext context) : base(context)
    {
        _context = context;
    }
}
