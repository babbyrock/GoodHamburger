using AutoMapper;
using GoodHamburger.Application.Common;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Result<GetOrderByIdResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetOrderByIdResponse>> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(request.OrderId, cancellationToken);
        if (order == null)
            return Result<GetOrderByIdResponse>.Failure("Pedido não encontrado.");

        var response = _mapper.Map<GetOrderByIdResponse>(order);
        return Result<GetOrderByIdResponse>.Success(response);
    }
}