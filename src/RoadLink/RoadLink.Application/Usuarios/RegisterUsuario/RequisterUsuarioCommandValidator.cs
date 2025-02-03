using FluentValidation;

namespace RoadLink.Application.Usuarios.RegisterUsuario;

internal sealed class RequisterUsuarioCommandValidator : AbstractValidator<RegisterUsuarioCommand>
{
    public RequisterUsuarioCommandValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre de usuario debe ser nula.");
        RuleFor(c => c.Apellido).NotEmpty().WithMessage("El apellido de debe ser nula.");
        RuleFor(c => c.Email).EmailAddress().NotEmpty().WithMessage("El email de debe ser nula.");
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8).WithMessage("El password debe ser nula.");
    }
}