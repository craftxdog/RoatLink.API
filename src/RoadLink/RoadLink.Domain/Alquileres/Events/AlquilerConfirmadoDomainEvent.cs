using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Alquileres.Events;

public sealed record AlquilerConfirmadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;
