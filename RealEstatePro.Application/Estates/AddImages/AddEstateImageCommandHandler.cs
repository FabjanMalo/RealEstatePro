using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Application.EstateImages;
using RealEstatePro.Domain.EstateImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.AddImages;
public class AddEstateImageCommandHandler
    (IApplicationContext _context,
    IEstateImageRepository _imageRepository)
    : IRequestHandler<AddEstateImageCommand>
{
    public async Task Handle(AddEstateImageCommand request, CancellationToken cancellationToken)
    {
        var estate = await _context.Estates
            .Include(e => e.EstateImages)
            .FirstOrDefaultAsync(x => x.Id == request.EstateId, cancellationToken)
            ?? throw new Exception("Estate not found.");


        if ((request.Images.Count + estate.EstateImages.Count) > 5)
        {
            throw new Exception("You are allowed to have a maximum of 5 photos for an estate.");
        }


        foreach (var image in request.Images)
        {
            var imagebyte = await EstateImage.ConvertToByteArray(image);

            var estateImg = EstateImage.CreateImage(estate.Id, imagebyte);

            await _imageRepository.Add(estateImg);
        }

        await _context.SaveChangesAsync(cancellationToken);

    }
}
