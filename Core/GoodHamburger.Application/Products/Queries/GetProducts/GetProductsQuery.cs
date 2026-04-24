using GoodHamburger.Application.Common;
using MediatR;

namespace GoodHamburger.Application.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<Result<List<GetProductsResponse>>>
{
}