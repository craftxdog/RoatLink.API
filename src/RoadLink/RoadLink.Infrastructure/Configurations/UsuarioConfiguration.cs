using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadLink.Domain.Usuarios;

namespace RoadLink.Infrastructure.Configurations;

public sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");
        builder.HasKey(u => u.Id);

        builder.Property(usuario => usuario.Id).HasConversion(UsuarioId => UsuarioId.Value, value => new UsuarioId(value));


        builder.Property(u => u.Nombre).HasMaxLength(200)
            .HasConversion(nombre => nombre!.Value, value => new Nombre(value));

        builder.Property(u => u.Apellido).HasMaxLength(200)
            .HasConversion(apellido => apellido!.Value, value => new Apellido(value));

        builder.Property(u => u.Email).HasMaxLength(400)
            .HasConversion(email => email!.Value, value => new Domain.Usuarios.Email(value));

        builder.HasIndex(usuario => usuario.Email).IsUnique();
    }
}
