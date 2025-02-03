using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadLink.Application.Abstractions.Clock;
using RoadLink.Application.Abstractions.Data;
using RoadLink.Application.Abstractions.Email;
using RoadLink.Domain.Abstractions;
using RoadLink.Domain.Alquileres;
using RoadLink.Domain.Usuarios;
using RoadLink.Domain.Vehiculos;
using RoadLink.Infrastructure.Clock;
using RoadLink.Infrastructure.Data;
using RoadLink.Infrastructure.Email;
using RoadLink.Infrastructure.Repositories;

namespace RoadLink.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration
        
        )
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailServices>();
        var connectionString = configuration.GetConnectionString("ConnectionString") ??
                               throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<IAlquilerRepository, AlquilerRepository>();
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }
}