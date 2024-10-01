using Confluent.Kafka;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RealEstatePro.Application.Abstractions.Contracts;
using RealEstatePro.Application.Abstractions.Contracts.AuthService;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Application.EstateImages;
using RealEstatePro.Application.Estates;
using RealEstatePro.Application.KafkaService;
using RealEstatePro.Application.Mail;
using RealEstatePro.Application.Users;
using RealEstatePro.Infrastructure.Contracts;
using RealEstatePro.Infrastructure.Contracts.AuthService;
using RealEstatePro.Infrastructure.EstateImages;
using RealEstatePro.Infrastructure.Estates;
using RealEstatePro.Infrastructure.KafkaService;
using RealEstatePro.Infrastructure.Mail;
using RealEstatePro.Infrastructure.Users;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using IEmailSender = RealEstatePro.Application.Mail.IEmailSender;


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

        services.AddScoped<IAuthManager, AuthManager>();

        services.AddScoped<IEstateRepository, EstateRepository>();
        services.AddScoped<IEstateImageRepository, EstateImageRepository>();

        //Email Section

        services.AddTransient<IEmailSender, EmailSender>();
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        services.Configure<Email>(configuration
            .GetSection("EmailTemplates")
            .GetSection("UserRegister"));

        return services;
    }

    public static IServiceCollection ConfigureKafkaServices(this IServiceCollection services)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:29092",
            AllowAutoCreateTopics = true,
            Acks = Acks.All
        };

        services.AddSingleton<IProducer<Null, string>>(new ProducerBuilder<Null, string>(config).Build());

        services.AddSingleton<IKafkaProducer, KafkaProducer>();

        return services;
    }

    public static IServiceCollection ConfigureJWT
        (this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtConfig");
        var secretKey = jwtSettings.GetSection("SecretKey").Value;

        services.AddAuthentication(options =>
         {
             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         })
      .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,

                    ValidateAudience = false,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

                    ClockSkew = TimeSpan.Zero
                };

            });

        return services;

    }

    public static void AddSwaggerAuth(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Enter just your token in the text input below.",
            Type = SecuritySchemeType.Http,
            In = ParameterLocation.Header,
            Scheme = "Bearer",
            Name = "Authorization",
            BearerFormat = "JWT"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        });
    }
}

