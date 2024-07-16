using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.Update;

public class UpdateProductCommandRequest : IRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    public int BrandId { get; set; }
    public IList<int> CategoryIds { get; set; }
}
