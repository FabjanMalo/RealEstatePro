using RealEstatePro.Domain.BoughtEstates;
using RealEstatePro.Domain.EstateImages;
using RealEstatePro.Domain.Reservations;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.Estates;
public class Estate
{
    [Key]
    public Guid Id { get; private set; }


    [ForeignKey(nameof(User))]
    public Guid UserId { get; private set; }
    public User User { get; private set; }


    public string Name { get; private set; }
    public EstateCategory EstateCategory { get; private set; }
    public string Address { get; private set; }
    public decimal Price { get; private set; }
    public string? Description { get; private set; }
    public decimal SurfaceArea { get; private set; }
    public int FloorNumber { get; private set; }

    public bool IsPromoted { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    //public List<ReservationEntity> ReservationEntities { get; private set; }
    public List<EstateImage> EstateImages { get; private set; }

  //  public BoughtEstate BoughtEstate { get; private set; }
}
