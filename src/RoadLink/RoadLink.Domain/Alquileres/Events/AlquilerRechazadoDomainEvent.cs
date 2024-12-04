using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Alquileres.Events;

public sealed record AlquilerRechazadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;
