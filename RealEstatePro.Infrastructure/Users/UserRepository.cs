using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Users;
using RealEstatePro.Domain.Users;
using RealEstatePro.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure.Users;

public class UserRepository : GenericRepository<User>, IUserRepository
{

    private readonly RealEstateDbContext _dbContext;

    public UserRepository(RealEstateDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
    {
        var isUnique = await _dbContext.Users
            .Where(x => x.Email == email)
            .ToListAsync(cancellationToken);


        return isUnique.Count == 0;
    }
}
