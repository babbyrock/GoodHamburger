using AutoMapper;
using GoodHamburger.Application.Common;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Products.Queries.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, Result<List<GetProductsResponse>>>
{
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductsHandler(IBaseRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetProductsResponse>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        var response = _mapper.Map<List<GetProductsResponse>>(products);
        return Result<List<GetProductsResponse>>.Success(response);
    }
}