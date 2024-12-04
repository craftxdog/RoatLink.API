using RoadLink.Application.Abstractions.Messaging;

namespace RoadLink.Application.Alquileres.ReservarAlquiler;

public record ReservarAlquilerCommand(
    Guid VehiculoId,
    Guid UsuarioId,
    DateOnly FechaInicio,
    DateOnly FechaFin
    ) : ICommand<Guid>;