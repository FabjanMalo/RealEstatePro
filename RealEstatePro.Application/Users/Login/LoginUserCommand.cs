using MediatR;
using RealEstatePro.Application.Models.Identity;
using RealEstatePro.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Users.Login;
public class LoginUserCommand : IRequest<Result<LoginUserResponse>>
{
    public LoginUserDto LoginUserDto { get; set; }
}
