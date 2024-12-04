using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadLink.Domain.Shared;
using RoadLink.Domain.Vehiculos;

namespace RoadLink.Infrastructure.Configurations;

public sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        builder.ToTable("vehiculos");
        builder.HasKey(x => x.Id);
        builder.OwnsOne(v => v.Direccion);
        builder.Property(vehiculo => vehiculo.Modelo).HasMaxLength(200).HasConversion(modelo => modelo!.Value, value => new Modelo(value));
        builder.Property(v => v.Vin).HasMaxLength(500).HasConversion(vin => vin!.Value, value => new Vin(value));
        builder.OwnsOne(v => v.Precio, priceBuilder =>
        {
            priceBuilder.Property(moneda => moneda.TipoMoneda).HasConversion(tipoMoneda => tipoMoneda.Codigo,
                codigo => TipoMoneda.FromCodigo(codigo!));
        });
        builder.OwnsOne(v => v.Mantenimiento,
            priceBuilder => priceBuilder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!)));
        builder.Property<uint>("Version").IsRowVersion();
        
    }
}