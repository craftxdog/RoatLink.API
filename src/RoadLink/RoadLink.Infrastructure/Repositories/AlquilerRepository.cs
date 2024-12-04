using Microsoft.EntityFrameworkCore;
using RoadLink.Domain.Alquileres;
using RoadLink.Domain.Vehiculos;

namespace RoadLink.Infrastructure.Repositories;

internal sealed class AlquilerRepository : Repository<Alquiler>, IAlquilerRepository
{
    private readonly AlquilerStatus[] ActiveAlquilerStatuses =
    {
        AlquilerStatus.Reservado,
        AlquilerStatus.Confirmado,
        AlquilerStatus.Completado
    };
    public AlquilerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo, 
        DateRange duracion, 
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Alquiler>().AnyAsync(a => a.VehiculoId == vehiculo.Id && 
                                                             a.DuracionAlquiler.Inicio <= a.DuracionAlquiler.Termino 
                                                             && a.DuracionAlquiler.Termino >= a.DuracionAlquiler.Inicio &&
                                                             ActiveAlquilerStatuses.Contains(a.Status), cancellationToken);
    }
}