using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoadLink.Application.Abstractions.Authentication;
using RoadLink.Domain.Usuarios;

namespace RoadLink.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)

    {
        _options = options.Value;
    }

    public Task<string> Generate(Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id!.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email!.ToString())
        };
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
        _options.Issuer,
        _options.Audience,
        claims,
        null,
        DateTime.Now.AddDays(365),
        signingCredentials
            );
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult<string>(tokenValue);
    }
}