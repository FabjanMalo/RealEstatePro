using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Estates;
using RealEstatePro.Domain.Estates;
using RealEstatePro.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure.Estates;
public class EstateRepository : GenericRepository<Estate>, IEstateRepository
{
    private readonly RealEstateDbContext _dbContext;
    public EstateRepository(RealEstateDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
    {
        var estate = await _dbContext.Estates
              .Where(x => x.Name == name)
              .ToListAsync(cancellationToken);

        return estate.Count == 0;
    }
}
