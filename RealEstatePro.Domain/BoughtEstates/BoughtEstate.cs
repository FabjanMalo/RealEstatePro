using RealEstatePro.Domain.Estates;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.BoughtEstates;
public class BoughtEstate
{
    [Key]
    public Guid Id { get; private set; }

    //[ForeignKey(nameof(Estate))]
    //public Guid EstateId { get; private set; }
    //public Estate Estate { get; private set; }


    //[ForeignKey(nameof(User))]
    //public Guid UserId { get; private set; }
    //public User User { get; set; }

    public DateTime PurchaseDate { get; private set; }
    public decimal Price { get; private set; }
    public string Notes { get; private set; }
}
