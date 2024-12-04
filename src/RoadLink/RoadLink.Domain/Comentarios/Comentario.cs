using RoadLink.Domain.Abstractions;
using RoadLink.Domain.Alquileres;
using RoadLink.Domain.Comentarios.Events;

namespace RoadLink.Domain.Comentarios;

public sealed class Comentario : Entity<ComentarioId>
{
    private Comentario()
    {
    }

    private Comentario(
        ComentarioId id,
        Vehiculos.VehiculoId vehiculoId,
        AlquilerId alquilerId,
        Usuarios.UsuarioId usuarioId,
        Rating rating,
        Descripcion description,
        DateTime? fechaHoraCreacion
        ) : base(id)
    {
        VehiculoId = vehiculoId;
        AlquilerId = alquilerId;
        UsuarioId = usuarioId;
        Rating = rating;
        Descripcion = description;
        FechaHoraCreacion = fechaHoraCreacion;
    }
    public Vehiculos.VehiculoId? VehiculoId { get; private set; }
    public AlquilerId? AlquilerId { get; private set; }
    public Usuarios.UsuarioId? UsuarioId { get; private set; }

    public Rating? Rating { get; private set; }
    public Descripcion? Descripcion { get; private set; }
    public DateTime? FechaHoraCreacion { get; private set; }

    public static Result<Comentario> Create(
        Alquiler alquiler,
        Rating rating,
        Descripcion description,
        DateTime? fechaHoraCreacion
        )
    {
        if (alquiler.Status != AlquilerStatus.Completado)
        {
            return Result.Failure<Comentario>(ComentarioErrors.NotEligibile);
        }

        var comentario = new Comentario(
            ComentarioId.New(),
            alquiler.VehiculoId!,
            alquiler.Id!,
            alquiler.UsuarioId!,
            rating,
            description,
            fechaHoraCreacion
        );
        comentario.RaiseDomainEvent(new ComentarioCreatedDomainEvent(comentario.Id!));
        return comentario;
    }
}
