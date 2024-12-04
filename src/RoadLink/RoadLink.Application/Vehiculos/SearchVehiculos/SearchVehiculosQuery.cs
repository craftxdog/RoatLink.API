using RoadLink.Application.Abstractions.Messaging;

namespace RoadLink.Application.Vehiculos.SearchVehiculos;

public sealed record SearchVehiculosQuery(
    DateOnly fechaInicio,
    DateOnly fechaFin
    ) : IQuery<IReadOnlyList<VehiculoResponse>>;