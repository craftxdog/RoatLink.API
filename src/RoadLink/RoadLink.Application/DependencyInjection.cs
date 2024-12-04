using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RoadLink.Application.Abstractions.Behaviors;
using RoadLink.Domain.Alquileres;

namespace RoadLink.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            c.AddOpenBehavior(typeof(LoggingBehavior<,>));
            c.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient<PrecioService>();
        return services;
    }
}