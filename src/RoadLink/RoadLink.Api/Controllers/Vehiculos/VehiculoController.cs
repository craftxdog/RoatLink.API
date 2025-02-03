using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadLink.Application.Vehiculos.SearchVehiculos;

namespace RoatLink.Api.Controllers.Vehiculos;

[ApiController]
[Route("api/vehiculos")]
public class VehiculoController : ControllerBase
{
    private readonly ISender _sender;

    public VehiculoController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> SearchVehiculos(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken
        )
    {
        var query = new SearchVehiculosQuery(startDate, endDate);
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}