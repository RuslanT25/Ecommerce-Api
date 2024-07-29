using Ecommerce.Application.Interfaces.RedisCache;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.GetAll;

public class GetAllProductsQueryRequest : IRequest<IList<GetAllProductsQueryResponse>>, ICacheableQuery
{
    public string CacheKey => "GetAllProducts";

    public double CacheTime => 60;
}
