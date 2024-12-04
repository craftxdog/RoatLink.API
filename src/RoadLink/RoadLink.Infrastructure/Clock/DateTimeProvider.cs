using RoadLink.Application.Abstractions.Clock;

namespace RoadLink.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime currentTime => DateTime.UtcNow;
}