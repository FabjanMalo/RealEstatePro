using RealEstatePro.Domain.Estates;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.Reservations;
public class ReservationEntity
{
    [Key]
    public Guid Id { get; private set; }


    [ForeignKey(nameof(User))]
    public Guid UserId { get; private set; }
    public User User { get; set; }


    public Guid EstateId { get; private set; }
    public Estate Estate { get; set; }

    public DateTime ReservationDate { get; private set; }
    public ReservationStatus ReservationStatus { get; private set; }

}
