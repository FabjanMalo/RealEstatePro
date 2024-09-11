using RealEstatePro.Domain.Estates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.EstateImages;
public class EstateImage
{
    [Key]
    public Guid Id { get; private set; }

    [ForeignKey(nameof(Estate))]
    public Guid EstateId { get; private set; }
    public Estate Estate { get; private set; }

    public byte[] Image { get; private set; }
}
