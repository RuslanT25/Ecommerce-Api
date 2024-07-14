using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Features.Products.Queries;

public class GetAllProductsQueryResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    public BrandDTO Brand { get; set; }
}
