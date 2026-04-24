using AutoMapper;
using GoodHamburger.Application.Common;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Result<UpdateOrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderHandler(
        IOrderRepository orderRepository,
        IBaseRepository<Product> productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UpdateOrderResponse>> Handle(
        UpdateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(request.OrderId, cancellationToken);
        if (order == null)
            return Result<UpdateOrderResponse>.Failure("Pedido não encontrado.");

        order.ClearItems();

        foreach (var productId in request.ProductIds)
        {
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product == null)
                return Result<UpdateOrderResponse>.Failure($"Produto com ID {productId} não encontrado.");

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Product = product,
                OrderId = order.Id
            };

            var (success, error) = order.AddItem(orderItem);
            if (!success)
                return Result<UpdateOrderResponse>.Failure(error!);
        }

        order.UpdatedAt = DateTime.UtcNow;
        _orderRepository.Update(order);
        await _unitOfWork.CommitAsync(cancellationToken);

        var response = _mapper.Map<UpdateOrderResponse>(order);
        return Result<UpdateOrderResponse>.Success(response);
    }
}