using AutoMapper;
using GoodHamburger.Application.Orders.Commands.CreateOrder;
using GoodHamburger.Application.Orders.Commands.UpdateOrder;
using GoodHamburger.Application.Orders.Queries.GetAllOrders;
using GoodHamburger.Application.Orders.Queries.GetOrderById;
using GoodHamburger.Application.Products.Queries.GetProducts;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Order mappings
        CreateMap<Order, CreateOrderResponse>();
        CreateMap<Order, UpdateOrderResponse>();
        CreateMap<Order, GetOrderByIdResponse>();
        CreateMap<Order, GetAllOrdersResponse>();

        CreateMap<Order, GetAllOrdersResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<OrderItem, GetAllOrdersItemResponse>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

        // Product mappings
        CreateMap<Product, GetProductsResponse>();

    }
}