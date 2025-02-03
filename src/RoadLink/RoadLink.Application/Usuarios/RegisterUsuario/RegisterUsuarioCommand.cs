using RoadLink.Application.Abstractions.Messaging;

namespace RoadLink.Application.Usuarios.RegisterUsuario;

public sealed record RegisterUsuarioCommand(string Email, string Nombre, string Apellido, string Password) : ICommand<Guid>;