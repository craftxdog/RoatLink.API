using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Vehiculos;

public static class VehiculosErrors
{
    public static Error NotFound = new(
        "Vehiculo.NotFound",
        "Vehiculo no encontrado con el Id."
    );
    
}