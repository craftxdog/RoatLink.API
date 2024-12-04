using RoadLink.Domain.Abstractions;
using RoadLink.Domain.Alquileres.Events;
using RoadLink.Domain.Shared;
using RoadLink.Domain.Vehiculos;

namespace RoadLink.Domain.Alquileres;
// Expert domain is how know all about the busyness.
public sealed class Alquiler : Entity
{
    private Alquiler()
    {
    }

    private Alquiler(
        Guid id,
        Guid vehiculoId,
        Guid usuarioId,
        DateRange duracionAlquiler,
        Moneda precioPorPeriodo,
        Moneda precioMantenimiento,
        Moneda accesorios,
        Moneda precioTotal,
        AlquilerStatus status,
        DateTime fechaCreacionAlquiler
    ) : base(id)
    {
        VehiculoId = vehiculoId;
        UsuarioId = usuarioId;
        DuracionAlquiler = duracionAlquiler;
        PrecioPorPeriodo = precioPorPeriodo;
        PrecioMantenimiento = precioMantenimiento;
        Accesorios = accesorios;
        PrecioTotal = precioTotal;
        Status = status;
        FechaCreacionAlquiler = fechaCreacionAlquiler;
    }
    public Guid VehiculoId { get; private set; }
    public Guid UsuarioId { get; private set; }
    
    public Moneda? PrecioPorPeriodo { get; private set; }
    public Moneda? PrecioMantenimiento { get; private set; }
    public Moneda? Accesorios { get; private set; }
    public Moneda? PrecioTotal { get; private set; }
    
    public AlquilerStatus Status { get; private set; }
    public DateRange DuracionAlquiler { get; private set; }
    
    public DateTime? FechaCreacionAlquiler { get; private set; }
    public DateTime? FechaConfirmacionAlquiler { get; private set; }
    public DateTime? FechaDeNegacion { get; private set; }
    public DateTime? FechaCompletoAlquiler { get; private set; }

    public DateTime? FechaCancelacionAlquiler { get; private set; }


    // We are going to need a DomainServerClass for calculate the rest of the attributes that my construct need.

    public static Alquiler Reservar(
        Vehiculo vehiculo,
        Guid usuarioId,
        DateRange duracionAlquiler,
        DateTime fechaCreacionAlquiler,
        PrecioService precioService
    )
    {
        var precioDetalle = precioService.CalcularPrecio(vehiculo, duracionAlquiler);
        var alquiler = new Alquiler(
            Guid.NewGuid(),
            vehiculo.Id,
            usuarioId,
            duracionAlquiler,
            precioDetalle.PrecioPorPeriodo,
            precioDetalle.Mantenimiento,
            precioDetalle.Accesorios,
            precioDetalle.PrecioTotal,
            AlquilerStatus.Reservado,
            fechaCreacionAlquiler
        );
        alquiler.RaiseDomainEvent(new AlquilerReservadoDomainEvent(alquiler.Id!));
        vehiculo.FechaUltimoAlquiler = fechaCreacionAlquiler;
        return alquiler;
    }

    public Result Confirmar(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerErrors.NotReserved);

        }

        Status = AlquilerStatus.Confirmado;
        FechaConfirmacionAlquiler = utcNow;
        RaiseDomainEvent(new AlquilerConfirmadoDomainEvent(Id));

        return Result.Success();
    }

    public Result Rechazar(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerErrors.NotReserved);
        }
        Status = AlquilerStatus.Rechazado;
        FechaDeNegacion = utcNow;
        RaiseDomainEvent(new AlquilerRechazadoDomainEvent(Id));
        return Result.Success();
    }

    public Result Cancelar(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Confirmado)
        {
            return Result.Failure(AlquilerErrors.NotConfirmed);
        }
        var currentDate = DateOnly.FromDateTime(utcNow);
        if (currentDate > DuracionAlquiler.Inicio)
        {
            return Result.Failure(AlquilerErrors.AlreadyStarted);
        }
        Status = AlquilerStatus.Cancelado;
        FechaCancelacionAlquiler = utcNow;
        RaiseDomainEvent(new AlquilerCanceladoDomainEvent(Id));
        return Result.Success();
    }
    
    public Result Completado(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Confirmado)
        {
            return Result.Failure(AlquilerErrors.NotConfirmed);
        }
        Status = AlquilerStatus.Completado;
        FechaCompletoAlquiler = utcNow;
        RaiseDomainEvent(new AlquilerCompletadoDomainEvent(Id));
        return Result.Success();
    }
}