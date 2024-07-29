using Ecommerce.Application.Features.Products.DTOs;

namespace Ecommerce.Application.Features.Products.Queries.GetAll;

public class GetAllProductsQueryResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    public BrandDTO Brand { get; set; }
    public List<CategoryDTO> Category { get; set; }
}
