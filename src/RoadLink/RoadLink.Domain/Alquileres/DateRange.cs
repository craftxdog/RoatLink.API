namespace RoadLink.Domain.Alquileres;

public record DateRange
{
    private DateRange()
    {
        
    }
    
    public DateOnly Inicio { get; init; }
    public DateOnly Termino { get; init; }
    
    // Lanpda function. 
    public int CantidadDias => Termino.DayNumber - Inicio.DayNumber;

    public static DateRange Create(DateOnly inicio, DateOnly termino)
    {
        if (inicio > termino)
        {
            throw new ApplicationException($"Inicio {inicio} - {termino}");
        }
        return new DateRange
        {
            Inicio = inicio,
            Termino = termino
        };
    }
}