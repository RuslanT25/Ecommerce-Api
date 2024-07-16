using Ecommerce.Application.Features.Products.Commands.Add;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.GetAll;

public class GetAllProductsQueryRequest : IRequest<IList<GetAllProductsQueryResponse>>
{
}
