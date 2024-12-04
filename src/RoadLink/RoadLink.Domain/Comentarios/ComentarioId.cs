namespace RoadLink.Domain.Comentarios;

public record ComentarioId(Guid Value)
{
    public static ComentarioId New() => new(Guid.NewGuid());
}
