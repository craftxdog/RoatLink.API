using RoadLink.Domain.Vehiculos;

namespace RoadLink.Domain.Alquileres;

public interface IAlquilerRepository
{
    Task<Alquiler?> GetByIdAsync(Guid alquilerId, CancellationToken cancellationToken = default);
    Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo,
        DateRange duracion,
        CancellationToken cancellationToken = default);
    void Add(Alquiler alquiler);
}
