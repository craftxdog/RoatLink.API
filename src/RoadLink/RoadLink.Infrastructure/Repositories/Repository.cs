using Microsoft.EntityFrameworkCore;
using RoadLink.Domain.Abstractions;

namespace RoadLink.Infrastructure.Repositories;

internal abstract class Repository<TEntity, TEntityId> where TEntity : Entity<TEntityId> where TEntityId : class
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(
        TEntityId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>().FirstOrDefaultAsync(usuario => usuario.Id == id, cancellationToken);
    }

    public void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }
}
