using Microsoft.EntityFrameworkCore;
using RoadLink.Domain.Usuarios;

namespace RoadLink.Infrastructure.Repositories;

internal sealed class UsuarioRepository : Repository<Usuario, UsuarioId>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Usuario?> GetByEmailAsync(Domain.Usuarios.Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Usuario>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> IUserExists(Domain.Usuarios.Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Usuario>().AnyAsync(x => x.Email == email, cancellationToken);
    }
}
