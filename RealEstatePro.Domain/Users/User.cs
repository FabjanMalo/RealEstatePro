using RealEstatePro.Domain.BoughtEstates;
using RealEstatePro.Domain.Estates;
using RealEstatePro.Domain.Reservations;
using RealEstatePro.Domain.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.Users;
public class User
{
    [Key]
    public Guid Id { get; private set; }


    [ForeignKey(nameof(UserRole))]
    public Guid UserRoleId { get; private set; }
    public UserRole UserRole { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }


    public List<Estate> Estates { get; private set; }
    public List<ReservationEntity> ReservationEntities { get; private set; }
   // public List<BoughtEstate> BoughtEstates { get; private set; }
}