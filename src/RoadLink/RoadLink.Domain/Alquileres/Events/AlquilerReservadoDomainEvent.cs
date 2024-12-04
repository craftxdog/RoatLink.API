using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Alquileres.Events;

public sealed record AlquilerReservadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;
