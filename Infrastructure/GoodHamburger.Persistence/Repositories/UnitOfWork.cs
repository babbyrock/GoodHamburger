using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Persistence;

public class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken) =>
        await db.SaveChangesAsync(cancellationToken);
}