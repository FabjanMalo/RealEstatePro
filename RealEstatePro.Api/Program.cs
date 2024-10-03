using RealEstatePro.Infrastructure;
using RealEstatePro.Application;
using RealEstatePro.Api.Middleware;
using RealEstatePro.Domain.Roles;
using RealEstatePro.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Hangfire;
using RealEstatePro.Infrastructure.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.ConfigureKafkaServices();

builder.Services.ConfigureApplicationServices();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddHangfire((sp, config) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("HangFireConnectingString");

    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UseSqlServerStorage(connectionString);
});
builder.Services.AddScoped<UserRecurringJobs>();
builder.Services.AddHangfireServer();

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

app.UseCors(x => x.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<UserRecurringJobs>("updateUser", x
    => x.UpdateUserNameWrapper("ani@gmail.com"), "*/3 * * * *");

RecurringJob.AddOrUpdate<UserRecurringJobs>("deleteUsers", x
    => x.DeleteInactiveUser(), Cron.Daily);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
