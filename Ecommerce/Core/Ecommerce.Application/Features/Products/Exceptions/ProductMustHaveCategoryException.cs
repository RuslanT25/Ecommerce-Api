using Ecommerce.Application.Bases;

namespace Ecommerce.Application.Features.Products.Exceptions;

public class ProductMustHaveCategoryException : BaseException
{
    public ProductMustHaveCategoryException(int id) : base($"Category with Id {id} does not exist.") { }
}
