using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Domain.BoughtEstates;
using RealEstatePro.Domain.EstateImages;
using RealEstatePro.Domain.Estates;
using RealEstatePro.Domain.Reservations;
using RealEstatePro.Domain.Roles;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure;
public class RealEstateDbContext : DbContext, IApplicationContext
{
    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options)
    { }

    public DbSet<Estate> Estates { get; set; }
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<ReservationEntity> Reservations { get; set; }
    public DbSet<EstateImage> EstateImages { get; set; }
    //public DbSet<BoughtEstate> BoughtEstates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
