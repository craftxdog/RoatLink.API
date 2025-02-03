namespace RoadLink.Domain.Usuarios;

public sealed class UserRole
{
    public int RoleId { get; set; }
    public UsuarioId? UserId { get; set; }
}