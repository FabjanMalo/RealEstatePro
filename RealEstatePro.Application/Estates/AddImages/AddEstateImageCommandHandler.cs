using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Application.EstateImages;
using RealEstatePro.Domain.Abstractions;
using RealEstatePro.Domain.EstateImages;
using RealEstatePro.Domain.Estates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.AddImages;
public class AddEstateImageCommandHandler
    (IApplicationContext _context,
    IEstateImageRepository _imageRepository)
    : IRequestHandler<AddEstateImageCommand, Result>
{
    public async Task<Result> Handle(AddEstateImageCommand request, CancellationToken cancellationToken)
    {
        var estate = await _context.Estates
            .Include(e => e.EstateImages)
            .FirstOrDefaultAsync(x => x.Id == request.EstateId, cancellationToken);

        if (estate is null)
        {
            return Result.Failure(EstateErrors.InvalidEstateId);
        }


        if ((request.Images.Count + estate.EstateImages.Count) > 5)
        {
            return Result.Failure(EstateErrors.ImageLimitExceeded);
        }


        foreach (var image in request.Images)
        {
            var imagebyte = await EstateImage.ConvertToByteArray(image);

            var estateImg = EstateImage.CreateImage(estate.Id, imagebyte);

            await _imageRepository.Add(estateImg);
        }


        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
