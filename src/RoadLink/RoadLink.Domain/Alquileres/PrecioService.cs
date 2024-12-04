using RoadLink.Domain.Shared;
using RoadLink.Domain.Vehiculos;

namespace RoadLink.Domain.Alquileres;

public class PrecioService
{
    public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
    {
        // To know the type of money. ($, EU..)
        var tipoMoneda = vehiculo.Precio!.TipoMoneda;
        var precioPorPeriodo = new Moneda(periodo.CantidadDias * vehiculo.Precio.Monto, tipoMoneda);

        // To count the total accessories.
        decimal porcentajeChange = 0;
        foreach (var accesorio in vehiculo.Accesorio!)
        {
            porcentajeChange += accesorio switch
            {
                Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                Accesorio.AireAcondicionado => 0.01m,
                Accesorio.Mapas => 0.01m,
                _ => 0
            };
        }

        var accesorioCharges = Moneda.Zero(tipoMoneda);

        if (porcentajeChange > 0)
        {
            accesorioCharges = new Moneda(precioPorPeriodo.Monto * porcentajeChange, tipoMoneda);
        }

        var precioTotal = Moneda.Zero();
        precioTotal += precioPorPeriodo;

        if (!vehiculo.Mantenimiento!.IsZero())
        {
            precioTotal += vehiculo.Mantenimiento;
        }

        precioTotal += accesorioCharges;

        return new PrecioDetalle(precioPorPeriodo, vehiculo.Mantenimiento, accesorioCharges, precioTotal);
    }
}