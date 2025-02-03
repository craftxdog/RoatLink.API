using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadLink.Domain.Permissions;

namespace RoadLink.Infrastructure.Configurations;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(permissionId => permissionId!.Value, value => new PermissionId(value));
        builder.Property(x => x.Nombre)
            .HasConversion(permissionNombre => permissionNombre!.Value, value => new Nombre(value));

        IEnumerable<Permission> permissions = Enum.GetValues<PermissionEnum>()
            .Select(p => new Permission(
                new PermissionId((int)p), 
                new Nombre(p.ToString()))
            );
        builder.HasData(permissions);
    }
}