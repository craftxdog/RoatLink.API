using MediatR;
using Microsoft.Extensions.Logging;
using RoadLink.Application.Abstractions.Messaging;

namespace RoadLink.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;
        try
        {
            _logger.LogInformation($"Handling request {requestName}.");
            var result = await next();
            _logger.LogInformation($"Handled request {requestName}.");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error handling request {requestName}.");
            throw;
        }
    }
}