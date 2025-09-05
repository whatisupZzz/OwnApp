using BuildingBlocks.Message.PersistMessage;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Message.PersistMessage;

public interface IPersistMessageDbContext
{
    DbSet<PersistMessage> PersistMessage { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task ExecuteTransactionalAsync(CancellationToken cancellationToken = default);
}