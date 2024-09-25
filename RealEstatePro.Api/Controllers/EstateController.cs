using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstatePro.Api.Extensions;
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

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }


        return Results.Ok(result.Value);
    }


    [HttpGet("getPromoted")]

    public async Task<IResult> GetPromoted()
    {
        var result = await _sender.Send(new GetPromotedEstatesQuery());

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok(result.Value);
    }


    [HttpPost("create")]

    public async Task<IResult> Create([FromForm] CreateEstateDto estateDto)
    {

        var command = new CreateEstateCommand { CreateEstateDto = estateDto };

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok(result.Value);
    }

    [HttpPost("addImages")]

    public async Task<IResult> AddImages(Guid estateId, [FromForm] List<IFormFile> images)
    {

        var command = new AddEstateImageCommand
        {
            EstateId = estateId,
            Images = images
        };

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }
        return Results.NoContent();
    }
}
