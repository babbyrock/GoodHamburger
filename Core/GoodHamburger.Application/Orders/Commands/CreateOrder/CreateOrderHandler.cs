using AutoMapper;
using GoodHamburger.Application.Common;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
{
    private readonly IBaseRepository<Order> _orderRepository;
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderHandler(
        IBaseRepository<Order> orderRepository,
        IBaseRepository<Product> productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CreateOrderResponse>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = new Order();

        foreach (var productId in request.ProductIds)
        {
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product == null)
                return Result<CreateOrderResponse>.Failure($"Produto com ID {productId} não encontrado.");

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Product = product
            };

            var (success, error) = order.AddItem(orderItem);
            if (!success)
                return Result<CreateOrderResponse>.Failure(error!);
        }

        await _orderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        var response = _mapper.Map<CreateOrderResponse>(order);
        return Result<CreateOrderResponse>.Success(response);
    }
}