using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Users.Register;
public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidation()
    {
        RuleFor(x => x.UserDto.Password)
           .MinimumLength(6)
           .Matches("[0-9]").WithMessage("Password must contain at least one numeric digit.")
           .Matches("[!@#$%^&*(),.?\":{}|<>]")
           .WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.UserDto.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Invalid email address format.");

        RuleFor(x => x.UserDto.FirstName)
              .NotEmpty().WithMessage("First name is required.")
              .MaximumLength(30);

        RuleFor(x => x.UserDto.LastName)
              .NotEmpty().WithMessage("Last name is required.")
              .MaximumLength(30);
    }
}
