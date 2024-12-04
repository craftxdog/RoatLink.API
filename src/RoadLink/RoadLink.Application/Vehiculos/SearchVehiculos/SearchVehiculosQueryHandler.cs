using Dapper;
using RoadLink.Application.Abstractions.Data;
using RoadLink.Application.Abstractions.Messaging;
using RoadLink.Domain.Abstractions;
using RoadLink.Domain.Alquileres;

namespace RoadLink.Application.Vehiculos.SearchVehiculos;

public sealed class SearchVehiculosQueryHandler : IQueryHandler<SearchVehiculosQuery, IReadOnlyList<VehiculoResponse>>
{
    private static readonly int [] ActiveAlquilerStatuses =
    {
        (int) AlquilerStatus.Reservado,
        (int) AlquilerStatus.Confirmado,
        (int) AlquilerStatus.Completado
    };
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchVehiculosQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(SearchVehiculosQuery request, CancellationToken cancellationToken)
    {
        if (request.fechaInicio > request.fechaFin)
        {
            return new List<VehiculoResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();
        const string sql = """
                           SELECT 
                               v.id AS Id,
                               v.modelo AS Modelo,
                               v.vin AS Vin,
                               v.precio_monto AS Precio,
                               v.precio_tipo_moneda AS TipoMoneda,
                               v.direccion_pais AS Pais,
                               v.direccion_departamento AS Departamento,
                               v.direccion_provincia AS Provincia,
                               v.direccion_ciudad AS Ciudad,
                               v.direccion_calle AS Calle
                           FROM vehiculos AS v WHERE NOT EXISTS(
                               SELECT 1 FROM alquileres AS q WHERE q.vehiculo_id = v.id AND 
                                                                   q.duracion_alquiler_inicio <= @EndDate AND 
                                                                   q.duracion_alquiler_termino >= @StartDate AND 
                                                                   q.status = ANY(@ActiveAlquilerStatuses)
                           )
                           """;
        var vehiculos = await connection.QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>
        (
            sql,
            (vehiculo, direccion) =>
            {
                vehiculo.Direccion = direccion;
                return vehiculo;
            },
            new
            {
                StartDate = request.fechaInicio,
                EndDate = request.fechaFin,
                ActiveAlquilerStatuses
            },
            splitOn: "Pais"
        );
        return vehiculos.ToList();
    }
}