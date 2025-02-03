using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Usuarios;

public static class UsuariosErrors
{
    public static Error NotFound = new(
        "Usuario.NotFound",
        "No existe el usuario buscado por id."
    );

    public static Error InvalidCredentials = new(
        "Usuario.InvalidCredentials",
        "Las credenciales son incorrectas."
    );
    public static Error AlreadyExists = new(
        "Usuario.AlreadyExists",
        "El usuario ya existe."
    );
}