using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using RentalCar.Security.Application.Handlers.Account;
using RentalCar.Security.Application.Handlers.Roles;
using RentalCar.Security.Application.Services;
using RentalCar.Security.Application.Validators.Login;

namespace RentalCar.Security.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddFluentValidation()
            .AddHandlers()
            .AddBackgroundService();
        return services;
    }
    
    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<LoginUserValidator>();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ChangePasswordHandler>());

        return services;
    }

    private static IServiceCollection AddBackgroundService(this IServiceCollection services)
    {
        services.AddHostedService<AccountConsumeService>();
        return services;
    }
    
}