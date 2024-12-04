using RoadLink.Domain.Usuarios;

namespace RoadLink.Infrastructure.Repositories;

internal sealed class UsuarioRepository : Repository<Usuario, UsuarioId>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
