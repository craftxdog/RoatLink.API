using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Comentarios.Events;

public sealed record ComentarioCreatedDomainEvent(Guid ComentarioId) : IDomainEvent;