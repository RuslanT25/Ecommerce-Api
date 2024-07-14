using MediatR;

namespace Ecommerce.Application.Features.Products.Queries;

public class GetAllProductsQueryRequest : IRequest<IList<GetAllProductsQueryResponse>>
{
}
