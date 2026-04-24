using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Persistence.Repositories;

public class OrderRepository(AppDbContext db) : BaseRepository<Order>(db), IOrderRepository
{
    public async Task<Order?> GetOrderWithItemsAsync(long orderId, CancellationToken cancellationToken) =>
        await _db.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

    public async Task<List<Order>> GetAllWithItemsAsync(CancellationToken cancellationToken) =>
        await _db.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
            .ToListAsync(cancellationToken);
}