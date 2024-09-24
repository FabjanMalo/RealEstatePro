using Microsoft.AspNetCore.Http;
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

    public EstateImage(Guid id, Guid estateId, byte[] image)
    {
        Id = id;
        EstateId = estateId;
        Image = image;
    }


    public static EstateImage CreateImage(Guid estateId, byte[] image)
    {
        var id = Guid.NewGuid();

        return new EstateImage(id, estateId, image);
    }

    public static async Task<byte[]> ConvertToByteArray(IFormFile file)
    {

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        return memoryStream.ToArray();
    }

    public static bool CheckFormat(IList<IFormFile> images)
    {
        foreach (var image in images)
        {
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
            {
                return false;
            }
        }
        return true;
    }

}