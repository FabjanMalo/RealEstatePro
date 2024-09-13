using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstatePro.Domain.BoughtEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure.Configurations;
//public class BoughtEstateConfiguration : IEntityTypeConfiguration<BoughtEstate>
//{
//    public void Configure(EntityTypeBuilder<BoughtEstate> builder)
//    {
//        builder
//            .HasOne(op => op.Estate)
//            .WithOne() many 
//            .OnDelete(DeleteBehavior.NoAction);

//        builder
//           .HasOne(op => op.User)
//           .WithMany()
//           .OnDelete(DeleteBehavior.NoAction);
//    }
//}
