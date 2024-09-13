using MediatR;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Users.Register;
public class RegisterUserCommand : IRequest<Guid>
{
    public UserDto UserDto { get; set; }
}
