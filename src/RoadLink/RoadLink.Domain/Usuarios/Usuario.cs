using RoadLink.Domain.Abstractions;
using RoadLink.Domain.Usuarios.Events;

namespace RoadLink.Domain.Usuarios;
// Buen ejemplo de encapsulacion.
public sealed class Usuario : Entity<UsuarioId>
{
    private Usuario()
    {
    }

    private Usuario(
        UsuarioId id,
        Nombre nombre,
        Apellido apellido,
        Email email
        ) : base(id)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
    }
    public Nombre? Nombre { get; private set; }
    public Apellido? Apellido { get; private set; }
    public Email? Email { get; private set; }

    public static Usuario Create(Nombre nombre, Apellido apellido, Email email)
    {
        var usuario = new Usuario(UsuarioId.New(), nombre, apellido, email);
        usuario.RaiseDomainEvent(new UserCreatedDomainEvent(usuario.Id!)); //This is a publisher "Publishing a event"
        return usuario;
    }
}