using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Usuarios.Events;

public record UserCreatedDomainEvent(UsuarioId UsuarioId) : IDomainEvent
{
    
}
