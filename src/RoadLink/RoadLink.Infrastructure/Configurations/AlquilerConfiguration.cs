using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadLink.Domain.Alquileres;
using RoadLink.Domain.Shared;
using RoadLink.Domain.Usuarios;
using RoadLink.Domain.Vehiculos;

namespace RoadLink.Infrastructure.Configurations;

public sealed class AlquilerConfiguration : IEntityTypeConfiguration<Alquiler>
{
    public void Configure(EntityTypeBuilder<Alquiler> builder)
    {
        builder.ToTable("alquileres");
        builder.HasKey(c => c.Id);

        builder.Property(alquiler => alquiler.Id).HasConversion(alquilerId => alquilerId.Value, value => new AlquilerId(value));

        builder.OwnsOne(a => a.PrecioPorPeriodo, precioBuilder =>
        {
            precioBuilder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        });

        builder.OwnsOne(a => a.PrecioMantenimiento, precioBuilder =>
        {
            precioBuilder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        });

        builder.OwnsOne(a => a.Accesorios, precioBuilder =>
        {
            precioBuilder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        });

        builder.OwnsOne(a => a.PrecioTotal, precioBuilder =>
        {
            precioBuilder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        });

        builder.OwnsOne(a => a.DuracionAlquiler);
        builder.HasOne<Vehiculo>().WithMany().HasForeignKey(a => a.VehiculoId);
        builder.HasOne<Usuario>().WithMany().HasForeignKey(a => a.UsuarioId);

    }
}
