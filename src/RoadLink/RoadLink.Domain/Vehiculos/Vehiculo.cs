using RoadLink.Domain.Abstractions;
using RoadLink.Domain.Shared;

namespace RoadLink.Domain.Vehiculos;

public sealed class Vehiculo : Entity
{
    private Vehiculo()
    {
    }

    public Vehiculo
    (
        Guid id,
        Modelo modelo,
        Vin vin,
        Moneda precio,
        Moneda mantenimiento,
        DateTime? fechaUltimoAlquiler,
        List<Accesorio> accesorios,
        Direccion direccion
    ) : base(id)
    {
        Modelo = modelo;
        Vin = vin;
        Precio = precio;
        Mantenimiento = mantenimiento;
        FechaUltimoAlquiler = fechaUltimoAlquiler;
        Direccion = direccion;
        Accesorio = accesorios;
    }
    public Modelo? Modelo { get; private set; }
    public Vin? Vin { get; private set; }
    public Moneda? Precio { get; private set; }
    public Moneda? Mantenimiento { get; private set; }
    public DateTime? FechaUltimoAlquiler { get; internal set; }
    public List<Accesorio> Accesorio { get; private set; }
    public Direccion? Direccion { get; private set; }
}