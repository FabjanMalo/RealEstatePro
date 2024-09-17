using MediatR;
using RealEstatePro.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Users.Login;
public class LoginUserCommand : IRequest<LoginUserResponse>
{
    public LoginUserDto LoginUserDto { get; set; }
}
