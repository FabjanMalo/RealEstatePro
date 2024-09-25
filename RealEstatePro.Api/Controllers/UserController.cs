using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstatePro.Api.Extensions;
using RealEstatePro.Application.Users.Login;
using RealEstatePro.Application.Users.Register;
using RealEstatePro.Domain.Abstractions;
using RealEstatePro.Domain.Users;

namespace RealEstatePro.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class UserController(ISender _sender) : ControllerBase
{

    [HttpPost("register")]

    public async Task<IResult> Register([FromBody] UserDto userDto)
    {

        var command = new RegisterUserCommand { UserDto = userDto };

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok(result.Value);
    }

    [HttpPost("login")]

    public async Task<IResult> Login([FromBody] LoginUserDto loginUserDto)
    {

        var command = new LoginUserCommand { LoginUserDto = loginUserDto };

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok(result.Value);
    }
}
