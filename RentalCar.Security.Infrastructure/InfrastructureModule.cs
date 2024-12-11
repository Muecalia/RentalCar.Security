using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Infrastructure.MessageBus;
using RentalCar.Security.Infrastructure.Persistence;
using RentalCar.Security.Infrastructure.Prometheus;
using RentalCar.Security.Infrastructure.Repositories;
using RentalCar.Security.Infrastructure.Services;

namespace RentalCar.Security.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
    {
        services
            .AddServices()
            .AddIdentityServices()
            .AddOpenTelemetryConfig();
        
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) 
    {
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IPrometheusService, PrometheusService>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        //CONFIG IDENTITY
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 4;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;

            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<AccountContext>()
        .AddDefaultTokenProviders();
        
        return services;
    }

    private static IServiceCollection AddOpenTelemetryConfig(this IServiceCollection services)
    {
        const string serviceName = "RentalCar Security";
        const string serviceVersion = "v1";
        
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithMetrics(metrics => metrics
                .AddConsoleExporter()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddPrometheusExporter()
            )
            .WithTracing(tracing => tracing
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(serviceName: serviceName, serviceVersion:serviceVersion))
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter()
                .AddConsoleExporter());

        return services;
    }
    
}