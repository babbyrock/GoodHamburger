using GoodHamburger.Application.Common;
using MediatR;

namespace GoodHamburger.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest<Result>
{
    public long OrderId { get; set; }

    public DeleteOrderCommand(long orderId)
    {
        OrderId = orderId;
    }
}