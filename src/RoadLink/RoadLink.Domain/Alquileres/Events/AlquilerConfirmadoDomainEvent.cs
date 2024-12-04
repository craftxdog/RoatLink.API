using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Alquileres.Events;

public sealed record AlquilerConfirmadoDomainEvent(Guid AlquilerId) : IDomainEvent;