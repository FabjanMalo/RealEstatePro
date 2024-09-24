using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstatePro.Application.Estates.AddImages;
using RealEstatePro.Application.Estates.Create;
using RealEstatePro.Application.Estates.GetAll;
using RealEstatePro.Application.Estates.GetPromoted;
using RealEstatePro.Application.Users.Register;
using RealEstatePro.Domain.Estates;
using RealEstatePro.Domain.Users;
using System.Net.WebSockets;

namespace RealEstatePro.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class EstateController(ISender _sender) : ControllerBase
{
    [HttpGet("getAll")]

    public async Task<IResult> GetAll()
    {
        var result = await _sender.Send(new GetAllEstateQuery());

        return Results.Ok(result);
    }


    [HttpGet("getPromoted")]

    public async Task<IResult> GetPromoted()
    {
        var result = await _sender.Send(new GetPromotedEstatesQuery());

        return Results.Ok(result);
    }


    [HttpPost("create")]

    public async Task<IResult> Create([FromForm] CreateEstateDto estateDto)
    {

        var command = new CreateEstateCommand { CreateEstateDto = estateDto };

        var result = await _sender.Send(command);

        return Results.Ok(result);
    }

    [HttpPost("addImages")]

    public async Task<IResult> AddImages(Guid estateId, [FromForm] List<IFormFile> images)
    {

        var command = new AddEstateImageCommand
        {
            EstateId = estateId,
            Images = images
        };

        await _sender.Send(command);

        return Results.NoContent();
    }
}
