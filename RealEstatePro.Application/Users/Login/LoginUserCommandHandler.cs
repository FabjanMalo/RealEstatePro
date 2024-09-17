using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Abstractions.Contracts.AuthService;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Users.Login;
public class LoginUserCommandHandler(
    IApplicationContext _context,
    IAuthManager _authManager)
    : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.LoginUserDto.Email);

        if (user is null)
        {
            throw new Exception($"User with {request.LoginUserDto.Email} not found.");
        }

        var isCorrectPassword = BCrypt.Net.BCrypt
            .EnhancedVerify(request.LoginUserDto.Password, user.Password);

        if (!isCorrectPassword)
        {
            throw new Exception("Incorrect Password");
        }

        var token = await _authManager.CreateToken(user);

        return new LoginUserResponse(token);
    }
}
