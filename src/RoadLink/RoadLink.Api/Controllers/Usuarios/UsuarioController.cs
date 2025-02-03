using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RoadLink.Application.Usuarios.LoginUsuario;
using RoadLink.Application.Usuarios.RegisterUsuario;

namespace RoatLink.Api.Controllers.Usuarios;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly ISender _sender;

    public UsuarioController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUsuarioRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }
        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUsuarioRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUsuarioCommand(
        request.Email,
        request.Nombre,
        request.Apellidos,
        request.Password
            );
        var result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }
        return Ok(result.Value);
    }
    
}