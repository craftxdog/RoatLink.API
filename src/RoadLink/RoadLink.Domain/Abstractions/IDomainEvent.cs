using MediatR;

namespace RoadLink.Domain.Abstractions;

// Publisher is how send events and subscriber is how reading the events
public interface IDomainEvent : INotification
{
    
}