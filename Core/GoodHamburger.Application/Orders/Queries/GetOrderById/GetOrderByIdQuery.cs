using GoodHamburger.Application.Common;
using MediatR;

namespace GoodHamburger.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQuery : IRequest<Result<GetOrderByIdResponse>>
{
    public long OrderId { get; set; }

    public GetOrderByIdQuery(long orderId)
    {
        OrderId = orderId;
    }
}