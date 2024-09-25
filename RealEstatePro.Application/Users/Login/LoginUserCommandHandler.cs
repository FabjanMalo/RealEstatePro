using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Abstractions.Contracts.AuthService;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Application.Models.Identity;
using RealEstatePro.Domain.Abstractions;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Users.Login;
public class LoginUserCommandHandler(
    IApplicationContext _context,
    IAuthManager _authManager)
    : IRequestHandler<LoginUserCommand, Result<LoginUserResponse>>
{
    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.LoginUserDto.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginUserResponse>(UserErrors.UserNotFound(request.LoginUserDto.Email));
        }

        var isCorrectPassword = BCrypt.Net.BCrypt
            .EnhancedVerify(request.LoginUserDto.Password, user.Password);

        if (!isCorrectPassword)
        {
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials);
        }

        var token = await _authManager.CreateToken(user);

        return new LoginUserResponse(token);
    }
}
