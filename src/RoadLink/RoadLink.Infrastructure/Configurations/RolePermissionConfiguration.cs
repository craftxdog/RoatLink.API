using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadLink.Domain.Permissions;
using RoadLink.Domain.Roles;

namespace RoadLink.Infrastructure.Configurations;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissions>
{
    public void Configure(EntityTypeBuilder<RolePermissions> builder)
    {
        builder.ToTable("roles_permissions");
        builder.HasKey(x => new { x.RoleID, x.PermissionID });
        builder.Property(x => x.PermissionID).HasConversion(permissionId => permissionId!.Value, value => new PermissionId(value));
        builder.HasData(Create(Role.Cliente, PermissionEnum.ReadUser), Create(Role.Admin, PermissionEnum.WriteUser), 
            Create(Role.Admin, PermissionEnum.UpdateUser), Create(Role.Admin, PermissionEnum.ReadUser));
    }

    private static RolePermissions Create(Role role, PermissionEnum permission)
    {
        return new RolePermissions
        {
            RoleID = role.Id,
            PermissionID = new PermissionId((int)permission)
        };
        
    }
}