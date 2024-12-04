using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Alquileres.Events;

public sealed record AlquilerCanceladoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;
