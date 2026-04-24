using GoodHamburger.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Persistence.Repositories;

public class BaseRepository<T>(AppDbContext db) : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _db = db;

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken) =>
        await _db.Set<T>().FindAsync([id], cancellationToken);

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken) =>
        await _db.Set<T>().ToListAsync(cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken) =>
        await _db.Set<T>().AddAsync(entity, cancellationToken);

    public void Update(T entity) =>
        _db.Set<T>().Update(entity);

    public void Remove(T entity) =>
        _db.Set<T>().Remove(entity);
}