namespace RoadLink.Domain.Usuarios;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(UsuarioId id, CancellationToken cancellationToken = default);
    void Add(Usuario user);
    
    Task<Usuario?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    
    Task<bool> IUserExists(Email email, CancellationToken cancellationToken = default);
}
