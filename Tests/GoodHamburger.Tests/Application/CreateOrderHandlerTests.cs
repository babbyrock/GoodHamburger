using AutoMapper;
using FluentAssertions;
using GoodHamburger.Application.Common.Mappings;
using GoodHamburger.Application.Orders.Commands.CreateOrder;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace GoodHamburger.Tests.Application;

public class CreateOrderHandlerTests
{
    private readonly Mock<IBaseRepository<Order>> _orderRepo = new();
    private readonly Mock<IBaseRepository<Product>> _productRepo = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly IMapper _mapper;
    private readonly CreateOrderHandler _handler;

    public CreateOrderHandlerTests()
    {
        var cfg = new MapperConfigurationExpression();
        cfg.AddProfile<MappingProfile>();
        _mapper = new Mapper(new MapperConfiguration(cfg, LoggerFactory.Create(_ => { })));
        _handler = new CreateOrderHandler(_orderRepo.Object, _productRepo.Object, _unitOfWork.Object, _mapper);
    }

    [Fact]
    public async Task Handle_valid_command_returns_success()
    {
        var product = new Product { Id = 1, Name = "X Burger", Price = 5.00m, Type = ProductType.Sandwich };
        _productRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(product);
        var result = await _handler.Handle(new CreateOrderCommand { ProductIds = [1] }, default);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_product_not_found_returns_failure()
    {
        _productRepo.Setup(r => r.GetByIdAsync(99, default)).ReturnsAsync((Product?)null);
        var result = await _handler.Handle(new CreateOrderCommand { ProductIds = [99] }, default);
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("99");
    }

    [Fact]
    public async Task Handle_duplicate_product_type_returns_failure()
    {
        var sandwich1 = new Product { Id = 1, Name = "X Burger", Price = 5.00m, Type = ProductType.Sandwich };
        var sandwich2 = new Product { Id = 2, Name = "X Egg", Price = 4.50m, Type = ProductType.Sandwich };
        _productRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(sandwich1);
        _productRepo.Setup(r => r.GetByIdAsync(2, default)).ReturnsAsync(sandwich2);
        var result = await _handler.Handle(new CreateOrderCommand { ProductIds = [1, 2] }, default);
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_valid_command_calls_commit()
    {
        var product = new Product { Id = 1, Name = "X Burger", Price = 5.00m, Type = ProductType.Sandwich };
        _productRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(product);
        await _handler.Handle(new CreateOrderCommand { ProductIds = [1] }, default);
        _unitOfWork.Verify(u => u.CommitAsync(default), Times.Once);
    }

    [Fact]
    public async Task Handle_product_not_found_does_not_commit()
    {
        _productRepo.Setup(r => r.GetByIdAsync(99, default)).ReturnsAsync((Product?)null);
        await _handler.Handle(new CreateOrderCommand { ProductIds = [99] }, default);
        _unitOfWork.Verify(u => u.CommitAsync(default), Times.Never);
    }
}