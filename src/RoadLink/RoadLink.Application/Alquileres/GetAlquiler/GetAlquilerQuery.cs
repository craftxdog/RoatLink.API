using RoadLink.Application.Abstractions.Messaging;

namespace RoadLink.Application.Alquileres.GetAlquiler;

public sealed record GetAlquilerQuery(Guid AlquilerId) : IQuery<AlquilerResponse>;
