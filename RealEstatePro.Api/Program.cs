using RealEstatePro.Infrastructure;
using RealEstatePro.Application;
using RealEstatePro.Api.Middleware;
using RealEstatePro.Domain.Roles;
using RealEstatePro.Domain.Users;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.ConfigureApplicationServices();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => options.AddSwaggerAuth());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
