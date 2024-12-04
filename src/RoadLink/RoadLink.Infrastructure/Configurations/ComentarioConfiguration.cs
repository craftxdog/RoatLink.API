using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadLink.Domain.Alquileres;
using RoadLink.Domain.Comentarios;
using RoadLink.Domain.Usuarios;
using RoadLink.Domain.Vehiculos;

namespace RoadLink.Infrastructure.Configurations;

public sealed class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
{
    public void Configure(EntityTypeBuilder<Comentario> builder)
    {
        builder.ToTable("comentarios");
        builder.HasKey(c => c.Id);

        builder.Property(comentario => comentario.Id).HasConversion(comentarioId => comentarioId.Value, value => new ComentarioId(value));


        builder.Property(c => c.Rating).HasConversion(rating => rating.Value, value => Rating.Create(value).Value);
        builder.Property(c => c.Descripcion).HasMaxLength(500).HasConversion(description => description.Value, value => new Descripcion(value));

        builder.HasOne<Vehiculo>().WithMany().HasForeignKey(c => c.VehiculoId);
        builder.HasOne<Alquiler>().WithMany().HasForeignKey(c => c.AlquilerId);
        builder.HasOne<Usuario>().WithMany().HasForeignKey(c => c.UsuarioId);
    }
}
