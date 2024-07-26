using Ecommerce.Application.Bases;

namespace Ecommerce.Application.Features.Products.Exceptions;

public class ProductMustHaveBrandException : BaseException
{
    public ProductMustHaveBrandException() : base("Product must have a valid existing brand.") { }
}
