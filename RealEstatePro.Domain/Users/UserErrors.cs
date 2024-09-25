using RealEstatePro.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.Users;
public static class UserErrors
{

    public static readonly Error EmailNotUnique =
        Error.Conflict("User.EmailNotUnique", "Email is not unique. Try another!");

    public static Error InvalidUserRole(string roleName) =>
        Error.BadRequest("User.InvalidUserRole", $"User role '{roleName}' does not exist.");

    public static readonly Error EmailSendingFailed =
        Error.Failure("User.EmailSendingFailed", " Failed to send confirmation email.");

    public static Error UserNotFound(string email) =>
    Error.NotFound("User.UserNotFound", $"User with email '{email}' not found.");

    public static readonly Error InvalidCredentials =
        Error.Unauthorized("User.InvalidCredentials", "Incorrect password. Please try again.");
}
