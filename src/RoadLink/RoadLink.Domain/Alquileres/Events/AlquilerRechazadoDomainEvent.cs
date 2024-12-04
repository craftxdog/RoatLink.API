using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Alquileres.Events;

public sealed record AlquilerRechazadoDomainEvent(Guid AlquilerId) : IDomainEvent;