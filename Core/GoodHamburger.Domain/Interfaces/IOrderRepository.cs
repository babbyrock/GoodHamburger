using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order?> GetOrderWithItemsAsync(long orderId, CancellationToken cancellationToken);
    Task<List<Order>> GetAllWithItemsAsync(CancellationToken cancellationToken);
}