using MediatR;
using RealEstatePro.Application.Models.Identity;
using RealEstatePro.Domain.Abstractions;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Users.Register;
public class RegisterUserCommand : IRequest<Result<Guid>>
{
    public UserDto UserDto { get; set; }
}
