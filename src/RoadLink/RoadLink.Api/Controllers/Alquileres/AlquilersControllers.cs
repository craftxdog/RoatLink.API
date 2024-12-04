using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoadLink.Application.Alquileres.GetAlquiler;
using RoadLink.Application.Alquileres.ReservarAlquiler;

namespace RoatLink.Api.Controllers.Alquileres;

[ApiController]
[Route("api/alquileres")]
public class AlquilersControllers : ControllerBase 
{
    private readonly ISender _sender;

    public AlquilersControllers(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAlquiler([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetAlquilerQuery(id);
        var result = await _sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ReservarAlquiler(Guid id, AlquilerReservaRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ReservarAlquilerCommand(
            request.VehiculoId,
            request.UsuarioId,
            request.StartDate,
            request.EndDate
        );
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetAlquiler), new { id = result.Value });
    }
}