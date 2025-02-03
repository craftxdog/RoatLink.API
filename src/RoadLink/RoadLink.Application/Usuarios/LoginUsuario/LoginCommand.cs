using RoadLink.Application.Abstractions.Messaging;

namespace RoadLink.Application.Usuarios.LoginUsuario;

public record LoginCommand(string Email, string Password) : ICommand<string>;