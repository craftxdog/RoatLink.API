using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Comentarios;

public static class ComentarioErrors
{
    public static readonly Error NotEligibile = new(
        "Comentario.NotEligibile",
        "Este comentario y calificacion no eligibile porque aun no se ha completado."
    );
}