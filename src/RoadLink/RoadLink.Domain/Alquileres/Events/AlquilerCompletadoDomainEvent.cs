using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Alquileres.Events;

public sealed record AlquilerCompletadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;
