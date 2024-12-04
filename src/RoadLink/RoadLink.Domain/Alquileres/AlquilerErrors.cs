using RoadLink.Domain.Abstractions;

namespace RoadLink.Domain.Alquileres;

public static class AlquilerErrors
{
    public static Error NotFound = new Error("Alquiler.Found", "Alquiler no fue encontrado.");
    public static Error Overlap = new Error("Alquiler.Overlap", "Alquiler se esta pidiendo por 2 o mas cientes en el mismo periodo.");
    public static Error NotReserved = new Error("Alquiler.NotReserved", "Alquiler no esta reservado.");
    public static Error NotConfirmed = new Error("Alquiler.NotConfirmed", "Alquiler no esta confirmado.");
    public static Error AlreadyStarted = new Error("Alquiler.AlreadyStarted", "Alquiler ya ha comenzado.");
}