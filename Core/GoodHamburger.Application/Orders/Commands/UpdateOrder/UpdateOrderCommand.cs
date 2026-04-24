using GoodHamburger.Application.Common;
using MediatR;

namespace GoodHamburger.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<Result<UpdateOrderResponse>>
{
    public long OrderId { get; set; }
    public List<long> ProductIds { get; set; } = new();
}