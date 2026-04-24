using GoodHamburger.Application.Common;
using MediatR;

namespace GoodHamburger.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersQuery : IRequest<Result<List<GetAllOrdersResponse>>>
{
}