using MediatR;
using RoadLink.Application.Abstractions.Email;
using RoadLink.Domain.Alquileres;
using RoadLink.Domain.Alquileres.Events;
using RoadLink.Domain.Usuarios;

namespace RoadLink.Application.Alquileres.ReservarAlquiler;

public sealed class ReservarAlquilerDomainHandler : INotificationHandler<AlquilerReservadoDomainEvent>
{
    public ReservarAlquilerDomainHandler(
        IAlquilerRepository alquilerRepository, 
        IUsuarioRepository usuarioRepository, 
        IEmailService emailService
        )
    {
        _alquilerRepository = alquilerRepository;
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }

    private readonly IAlquilerRepository _alquilerRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;
    
    public async Task Handle(AlquilerReservadoDomainEvent notification, CancellationToken cancellationToken)
    {
        var alquiler = await _alquilerRepository.GetByIdAsync(notification.AlquilerId, cancellationToken);
        if (alquiler is null)
        {
            return;
        }
        var usuario = await _usuarioRepository.GetByIdAsync(alquiler.UsuarioId, cancellationToken);
        if (usuario is null)
        {
            return;
        }

        await _emailService.SendEmailAsync(
        usuario.Email!,
        "Alquiler reservado",
        "Tines que confirmar esta reserva de lo contrario se perdera."
        );
    }
}