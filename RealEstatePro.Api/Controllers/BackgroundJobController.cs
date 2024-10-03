using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstatePro.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class BackgroundJobController() : ControllerBase
{

    [HttpPost("Schedule")]

    public IResult CreateScheduleJob()
    {
        var time = DateTime.Now.AddSeconds(5);
        var timeOffset = new DateTimeOffset(time);

        BackgroundJob.Schedule(() => Console.WriteLine($"Hello{DateTime.Now.Second}"), timeOffset);

        return Results.Ok();
    }
}
