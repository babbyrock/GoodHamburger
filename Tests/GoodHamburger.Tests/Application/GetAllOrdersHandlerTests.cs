using AutoMapper;
using FluentAssertions;
using GoodHamburger.Application.Common.Mappings;
using GoodHamburger.Application.Orders.Queries.GetAllOrders;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace GoodHamburger.Tests.Application;

public class GetAllOrdersHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepo = new();
    private readonly IMapper _mapper;
    private readonly GetAllOrdersHandler _handler;

    public GetAllOrdersHandlerTests()
    {
        var cfg = new MapperConfigurationExpression();
        cfg.AddProfile<MappingProfile>();
        _mapper = new Mapper(new MapperConfiguration(cfg, LoggerFactory.Create(_ => { })));
        _handler = new GetAllOrdersHandler(_orderRepo.Object, _mapper);
    }

    [Fact]
    public async Task Handle_returns_all_orders()
    {
        _orderRepo.Setup(r => r.GetAllWithItemsAsync(default))
            .ReturnsAsync([new Order { Id = 1 }, new Order { Id = 2 }]);
        var result = await _handler.Handle(new GetAllOrdersQuery(), default);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_empty_list_returns_success_with_empty()
    {
        _orderRepo.Setup(r => r.GetAllWithItemsAsync(default)).ReturnsAsync([]);
        var result = await _handler.Handle(new GetAllOrdersQuery(), default);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}