using RoadLink.Application.Abstractions.Clock;
using RoadLink.Application.Abstractions.Messaging;
using RoadLink.Application.Exceptions;
using RoadLink.Domain.Abstractions;
using RoadLink.Domain.Alquileres;
using RoadLink.Domain.Usuarios;
using RoadLink.Domain.Vehiculos;

namespace RoadLink.Application.Alquileres.ReservarAlquiler;

internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
{
    public ReservarAlquilerCommandHandler(
        IUsuarioRepository usuarioRepository,
        IVehiculoRepository vehiculoRepository,
        IAlquilerRepository alquilerRepository,
        PrecioService precioService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider
        )
    {
        _usuarioRepository = usuarioRepository;
        _vehiculoRepository = vehiculoRepository;
        _alquilerRepository = alquilerRepository;
        _precioService = precioService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IVehiculoRepository _vehiculoRepository;
    private readonly IAlquilerRepository _alquilerRepository;
    private readonly PrecioService _precioService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public async Task<Result<Guid>> Handle(
        ReservarAlquilerCommand request,
        CancellationToken cancellationToken)
    {
        var usuarioId = new UsuarioId(request.UsuarioId);
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId, cancellationToken);
        if (usuario is null)
        {
            return Result.Failure<Guid>(UsuariosErrors.NotFound);
        }
        var vehiculoId = new VehiculoId(request.VehiculoId);
        var vehiculo = await _vehiculoRepository.GetByIdAsync(vehiculoId, cancellationToken);
        if (vehiculo is null)
        {
            return Result.Failure<Guid>(VehiculosErrors.NotFound);
        }

        var duracion = DateRange.Create(request.FechaInicio, request.FechaFin);
        if (await _alquilerRepository.IsOverlappingAsync(vehiculo, duracion, cancellationToken))
        {
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }


        try
        {
            var alquiler = Alquiler.Reservar(vehiculo, usuario.Id!, duracion, _dateTimeProvider.currentTime, _precioService);
            _alquilerRepository.Add(alquiler);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return alquiler.Id!.Value;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }


    }
}
