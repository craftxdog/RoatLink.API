using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Alquileres.Events;

public sealed record AlquilerReservadoDomainEvent(Guid AlquilerId) : IDomainEvent;