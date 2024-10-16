﻿using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstatePro.Application.Abstractions.Contracts.AuthService;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Application.Mail;
using RealEstatePro.Application.Models.Identity;
using RealEstatePro.Domain.Abstractions;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Users.Register;
public class RegisterUserCommandHandler
    (IApplicationContext _context,
    IUserRepository _userRepository,
    IEmailSender _emailSender
   )
    : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var isUnique = await _userRepository.IsEmailUnique(request.UserDto.Email, cancellationToken);


        if (!isUnique)
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var password = BCrypt.Net.BCrypt.EnhancedHashPassword(request.UserDto.Password, 13);

        request.UserDto.Password = password;

        var userRole = await _context.UserRoles
            .FirstOrDefaultAsync(
            r => r.Name.Contains(request.UserDto.UserRoleName), cancellationToken);



        if (userRole is null)
        {
            return Result.Failure<Guid>(UserErrors.InvalidUserRole(request.UserDto.UserRoleName));
        }

        var user = User.CreateUser(request.UserDto, userRole.Id);

        try
        {
            var re = await _emailSender.SendEmail(user);

        }
        catch (Exception)
        {

            return Result.Failure<Guid>(UserErrors.EmailSendingFailed);
        }

        await _userRepository.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;

    }
}
