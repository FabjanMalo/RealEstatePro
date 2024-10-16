﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Domain.Abstractions;
using RealEstatePro.Domain.EstateImages;
using RealEstatePro.Domain.Estates;

namespace RealEstatePro.Application.Estates.Create;
public class CreateEstateCommandHandler
      (IApplicationContext _context,
       IEstateRepository _estateRepository)
    : IRequestHandler<CreateEstateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateEstateCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
           .FirstOrDefaultAsync(u => u.Id == request.CreateEstateDto.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(EstateErrors.InvalidUserId);
        }

        var isNameUnique = await _estateRepository
            .IsUniqueName(request.CreateEstateDto.Name, cancellationToken);

        if (!isNameUnique)
        {
            Result.Failure<Guid>(EstateErrors.NameNotUnique);
        }

        var estate = Estate.CreateEstate(request.CreateEstateDto);

        var estateImages = new List<EstateImage>();

        foreach (var image in request.CreateEstateDto.Images)
        {
            var imagebyte = await EstateImage.ConvertToByteArray(image);

            var estateImage = EstateImage.CreateImage(estate.Id, imagebyte);


            estateImages.Add(estateImage);

        }

        estate.EstateImages.AddRange(estateImages);


        user.Estates.Add(estate);

        await _estateRepository.Add(estate);

        await _context.SaveChangesAsync(cancellationToken);

        return estate.Id;
    }
}
