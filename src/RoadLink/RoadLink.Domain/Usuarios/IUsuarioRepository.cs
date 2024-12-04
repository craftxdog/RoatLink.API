namespace RoadLink.Domain.Usuarios;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(UsuarioId id, CancellationToken cancellationToken = default);
    void Add(Usuario user);
}
