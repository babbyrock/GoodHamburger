using AutoMapper;
using FluentAssertions;
using GoodHamburger.Application.Common.Mappings;
using GoodHamburger.Application.Orders.Queries.GetOrderById;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace GoodHamburger.Tests.Application;

public class GetOrderByIdHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepo = new();
    private readonly IMapper _mapper;
    private readonly GetOrderByIdHandler _handler;

    public GetOrderByIdHandlerTests()
    {
        var cfg = new MapperConfigurationExpression();
        cfg.AddProfile<MappingProfile>();
        _mapper = new Mapper(new MapperConfiguration(cfg, LoggerFactory.Create(_ => { })));
        _handler = new GetOrderByIdHandler(_orderRepo.Object, _mapper);
    }

    [Fact]
    public async Task Handle_existing_order_returns_success()
    {
        _orderRepo.Setup(r => r.GetOrderWithItemsAsync(1, default)).ReturnsAsync(new Order { Id = 1 });
        var result = await _handler.Handle(new GetOrderByIdQuery(1), default);
        result.IsSuccess.Should().BeTrue();
        result.Value!.Id.Should().Be(1);
    }

    [Fact]
    public async Task Handle_nonexistent_order_returns_failure()
    {
        _orderRepo.Setup(r => r.GetOrderWithItemsAsync(99, default)).ReturnsAsync((Order?)null);
        var result = await _handler.Handle(new GetOrderByIdQuery(99), default);
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("não encontrado");
    }
}