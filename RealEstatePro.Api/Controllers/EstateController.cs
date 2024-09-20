using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstatePro.Application.Estates.Create;
using RealEstatePro.Application.Users.Register;
using RealEstatePro.Domain.Estates;
using RealEstatePro.Domain.Users;

namespace RealEstatePro.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class EstateController(ISender _sender) : ControllerBase
{
    [HttpPost]

    public async Task<IResult> Create([FromForm] CreateEstateDto estateDto)
    {

        var command = new CreateEstateCommand { CreateEstateDto = estateDto };

        var result = await _sender.Send(command);

        return Results.Ok(result);
    }
}
