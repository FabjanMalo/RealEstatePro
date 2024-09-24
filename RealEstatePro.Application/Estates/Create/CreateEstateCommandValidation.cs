using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates.Create;
public class CreateEstateCommandValidation : AbstractValidator<CreateEstateCommand>
{
    public CreateEstateCommandValidation()
    {
        RuleFor(x => x.CreateEstateDto.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.CreateEstateDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name can not exceed 50 characters.");

        RuleFor(x => x.CreateEstateDto.EstateCategory)
            .NotNull().WithMessage("Estate category is required.");

        RuleFor(x => x.CreateEstateDto.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200).WithMessage("Address can not exceed 200 characters.");

        RuleFor(x => x.CreateEstateDto.Price)
            .NotEmpty().WithMessage("Price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than zero..");

        RuleFor(x => x.CreateEstateDto.Description)
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

        RuleFor(x => x.CreateEstateDto.SurfaceArea)
            .GreaterThan(0).WithMessage("Surface area must be greater than zero.");

        RuleFor(x => x.CreateEstateDto.FloorNumber)
            .GreaterThanOrEqualTo(0).WithMessage("Floor number must be a non-negative number.");

        RuleFor(x => x.CreateEstateDto.Images)
            .NotNull()
            .Must(images => images.Count >= 2 && images.Count <= 5)
            .WithMessage("You must upload between 2 and 5 images.")
            .Must(CheckFormat).WithMessage("Only .jpg, .jpeg, and .png formats are allowed.");





    }
    private bool CheckFormat(IList<IFormFile> images)
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
