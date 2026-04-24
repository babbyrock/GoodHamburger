using FluentAssertions;
using GoodHamburger.Application.Orders.Commands.DeleteOrder;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using Moq;

namespace GoodHamburger.Tests.Application;

public class DeleteOrderHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepo = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly DeleteOrderHandler _handler;

    public DeleteOrderHandlerTests()
    {
        _handler = new DeleteOrderHandler(_orderRepo.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task Handle_existing_order_returns_success()
    {
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(new Order { Id = 1 });

        var result = await _handler.Handle(new DeleteOrderCommand(1), default);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_nonexistent_order_returns_failure()
    {
        _orderRepo.Setup(r => r.GetByIdAsync(99, default)).ReturnsAsync((Order?)null);

        var result = await _handler.Handle(new DeleteOrderCommand(99), default);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("não encontrado");
    }

    [Fact]
    public async Task Handle_existing_order_calls_remove_and_commit()
    {
        var order = new Order { Id = 1 };
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);

        await _handler.Handle(new DeleteOrderCommand(1), default);

        _orderRepo.Verify(r => r.Remove(order), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_nonexistent_order_does_not_commit()
    {
        _orderRepo.Setup(r => r.GetByIdAsync(99, default)).ReturnsAsync((Order?)null);

        await _handler.Handle(new DeleteOrderCommand(99), default);

        _unitOfWork.Verify(u => u.CommitAsync(default), Times.Never);
    }
}