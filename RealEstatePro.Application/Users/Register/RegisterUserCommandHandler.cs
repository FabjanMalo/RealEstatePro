using BCrypt.Net;
using MediatR;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Users.Register;
public class RegisterUserCommandHandler
    (IApplicationContext _context,
    IUserRepository _userRepository)
    : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _userRepository.IsEmailUnique(request.UserDto.Email, cancellationToken);

        if (!isUnique)
        {
            throw new Exception("Email is not unique. Try another!");
        }

        var password = BCrypt.Net.BCrypt.EnhancedHashPassword(request.UserDto.Password, 13);

        request.UserDto.Password = password;

        var user = User.CreateUser(request.UserDto);

        await _userRepository.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
