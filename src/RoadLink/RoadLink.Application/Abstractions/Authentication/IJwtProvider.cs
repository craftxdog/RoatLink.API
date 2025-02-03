using RoadLink.Domain.Usuarios;

namespace RoadLink.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    Task<string> Generate(Usuario usuario);
}