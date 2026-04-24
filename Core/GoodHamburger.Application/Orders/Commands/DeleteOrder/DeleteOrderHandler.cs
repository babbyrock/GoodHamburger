using GoodHamburger.Application.Common;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
            return Result.Failure("Pedido não encontrado.");

        _orderRepository.Remove(order);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}