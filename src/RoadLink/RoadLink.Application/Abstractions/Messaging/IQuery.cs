
using MediatR;
using RoadLink.Domain.Abstractions;

namespace RoadLink.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> 
{

}
