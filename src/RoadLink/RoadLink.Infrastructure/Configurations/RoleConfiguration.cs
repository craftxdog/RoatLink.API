using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadLink.Domain.Roles;

namespace RoadLink.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(x => x.Id);
        builder.HasData(Role.GetValues());
        builder.HasMany(x => x.Permissions).WithMany().UsingEntity<RolePermissions>();
    }
}