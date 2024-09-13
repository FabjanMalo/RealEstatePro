using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstatePro.Application.Abstractions.Contracts;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Application.Users;
using RealEstatePro.Infrastructure.Contracts;
using RealEstatePro.Infrastructure.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure;
public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RealEstateDbContext>(
            op => op.UseSqlServer(
                configuration.GetConnectionString("RealEstateConnectingString")));

        services.AddScoped<IApplicationContext>
            (op => op.GetRequiredService<RealEstateDbContext>());

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
