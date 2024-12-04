namespace RoatLink.Api.Controllers.Alquileres;

public sealed record AlquilerReservaRequest(
    Guid VehiculoId,
    Guid UsuarioId,
    DateOnly StartDate,
    DateOnly EndDate
    );