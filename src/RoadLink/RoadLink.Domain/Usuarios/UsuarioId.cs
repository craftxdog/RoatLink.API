namespace RoadLink.Domain.Usuarios;

public record UsuarioId(Guid Value)
{
    public static UsuarioId New() => new(Guid.NewGuid());
}