using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadLink.Domain.Usuarios;

namespace RoadLink.Infrastructure.Configurations;

public sealed class UsuarioRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_role");
        builder.HasKey(x => new { x.RoleId, x.UserId });
        builder.Property(usuario => usuario.UserId)
            .HasConversion(userId => userId!.Value, value => new UsuarioId(value));
    }
}