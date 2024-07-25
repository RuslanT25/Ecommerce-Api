using Ecommerce.Application.Features.Products.Queries.GetAll;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.Create;

public class CreateProductCommandRequest : IRequest<GetAllProductsQueryResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    public int BrandId { get; set; }
    public IList<int> CategoryIds { get; set; }
}
