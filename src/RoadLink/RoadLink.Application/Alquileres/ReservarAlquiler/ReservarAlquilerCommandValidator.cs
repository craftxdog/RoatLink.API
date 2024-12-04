using FluentValidation;

namespace RoadLink.Application.Alquileres.ReservarAlquiler;

public class ReservarAlquilerCommandValidator: AbstractValidator<ReservarAlquilerCommand>
{
    public ReservarAlquilerCommandValidator()
    {
        RuleFor(c => c.UsuarioId).NotEmpty();
        RuleFor(c => c.VehiculoId).NotEmpty();
        RuleFor(c => c.FechaInicio).LessThan(C => C.FechaFin);
    }
}