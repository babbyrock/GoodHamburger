namespace GoodHamburger.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    void Remove(T entity);
}