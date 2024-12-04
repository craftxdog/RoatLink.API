namespace RoadLink.Domain.Alquileres;

public record AlquilerId(Guid Value)
{
    public static AlquilerId New() => new(Guid.NewGuid());
}
