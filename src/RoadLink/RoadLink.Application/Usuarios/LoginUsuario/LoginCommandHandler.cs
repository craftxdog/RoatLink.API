using RoadLink.Application.Abstractions.Authentication;
using RoadLink.Application.Abstractions.Messaging;
using RoadLink.Domain.Abstractions;
using RoadLink.Domain.Usuarios;

namespace RoadLink.Application.Usuarios.LoginUsuario;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUsuarioRepository usuarioRepository, IJwtProvider jwtProvider)
    {
        _usuarioRepository = usuarioRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // 1. Verify is the user exist. 2. compare the password. 3.Generated JWT. 4.Return that jwt generated.
        var usuario = await _usuarioRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
        if (usuario is null)
        {
            return Result.Failure<string>(UsuariosErrors.NotFound);
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash!.Value))
        {
            return Result.Failure<string>(UsuariosErrors.InvalidCredentials);
        }

        var token = await _jwtProvider.Generate(usuario);
        return token;
    }
}