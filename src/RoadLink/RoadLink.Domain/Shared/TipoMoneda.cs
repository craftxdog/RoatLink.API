namespace RoadLink.Domain.Shared;

public record TipoMoneda
{
    public static readonly TipoMoneda None = new("");
    public static readonly TipoMoneda Usd = new("USD");
    public static readonly TipoMoneda Cord = new("CORD");
    public static readonly TipoMoneda Eur = new("EUR");
    private TipoMoneda(string codigo) => Codigo = codigo;
    public string? Codigo { get; init; }

    public static readonly IReadOnlyCollection<TipoMoneda> All = new[]
    {
        Usd, Cord, Eur
    };

    public static TipoMoneda FromCodigo(string codigo)
    {
        return All.FirstOrDefault(c => c.Codigo == codigo) ?? throw new ApplicationException("Tipo de moneda no valido.");
    }

}