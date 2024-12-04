using MediatR;
using RoadLink.Domain.Abstractions;

namespace RoadLink.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{

}
