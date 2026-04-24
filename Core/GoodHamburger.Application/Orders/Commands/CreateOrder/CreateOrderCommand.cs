using GoodHamburger.Application.Common;
using MediatR;

namespace GoodHamburger.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Result<CreateOrderResponse>>
{
    public List<long> ProductIds { get; set; } = new();
}