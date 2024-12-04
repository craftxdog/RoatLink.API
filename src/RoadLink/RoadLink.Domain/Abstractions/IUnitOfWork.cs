namespace RoadLink.Domain.Abstractions;

// This take all the changes and inserted in the database.
public interface IUnitOfWork
{
    // CancellationToken is important because the database is not in the same level so this abort the task and start again.
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}