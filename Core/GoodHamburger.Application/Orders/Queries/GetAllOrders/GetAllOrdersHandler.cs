using AutoMapper;
using GoodHamburger.Application.Common;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, Result<List<GetAllOrdersResponse>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetAllOrdersHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllOrdersResponse>>> Handle(
        GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllWithItemsAsync(cancellationToken);
        var response = _mapper.Map<List<GetAllOrdersResponse>>(orders);
        return Result<List<GetAllOrdersResponse>>.Success(response);
    }
}