using RoadLink.Domain.Permissions;

namespace RoadLink.Domain.Roles;

public sealed class RolePermissions
{
    public int RoleID { get; set; }
    public PermissionId? PermissionID { get; set; }
}