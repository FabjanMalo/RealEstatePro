using Microsoft.EntityFrameworkCore;
using RealEstatePro.Domain.Estates;
using RealEstatePro.Domain.Roles;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Abstractions.Database;
public interface IApplicationContext
{
    public DbSet<User> Users { get; }
    public DbSet<UserRole> UserRoles { get; }
    public DbSet<Estate> Estates { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
