using RoadLink.Domain.Vehiculos;

namespace RoadLink.Infrastructure.Repositories;

internal sealed class VehiculoRepository : Repository<Vehiculo, VehiculoId>, IVehiculoRepository
{
    public VehiculoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
