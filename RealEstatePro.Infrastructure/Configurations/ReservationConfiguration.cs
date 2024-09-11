using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstatePro.Domain.BoughtEstates;
using RealEstatePro.Domain.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure.Configurations;
public class ReservationConfiguration : IEntityTypeConfiguration<ReservationEntity>
{
    public void Configure(EntityTypeBuilder<ReservationEntity> builder)
    {
        builder
            .HasOne(op => op.Estate)
            .WithMany()
            .HasForeignKey(x => x.EstateId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
