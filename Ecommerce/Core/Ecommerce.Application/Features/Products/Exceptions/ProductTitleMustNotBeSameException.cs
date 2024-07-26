using Ecommerce.Application.Bases;

namespace Ecommerce.Application.Features.Products.Exceptions;

public class ProductTitleMustNotBeSameException : BaseException
{
    public ProductTitleMustNotBeSameException() : base("Title already exists") { }
}
