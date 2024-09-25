using FluentValidation;
using RealEstatePro.Domain.EstateImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.AddImages;
public class AddEstateImageValidation : AbstractValidator<AddEstateImageCommand>
{
    public AddEstateImageValidation()
    {
        RuleFor(x => x.EstateId)
            .NotEmpty().WithMessage("Estate ID is required.");

        RuleFor(x => x.Images)
            .NotNull()
            .Must(images => EstateImage.CheckFormat(images))
            .WithMessage("Only .jpg, .jpeg, and .png formats are allowed.");
    }
}
