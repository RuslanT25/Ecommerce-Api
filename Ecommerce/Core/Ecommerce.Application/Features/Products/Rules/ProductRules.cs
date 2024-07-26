using Ecommerce.Application.Bases;
using Ecommerce.Application.Features.Products.Exceptions;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Application.Features.Products.Rules;

public class ProductRules : BaseRules
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductRules(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task ProductTitleMustNotBeSame(IList<Product> products, string requestTitle)
    {
        if (products.Any(x => x.Title == requestTitle)) throw new ProductTitleMustNotBeSameException();
        return Task.CompletedTask;
    }

    public async Task ProductMustHaveBrand(int brandId)
    {
        var brandExists = await _unitOfWork.GetReadRepository<Brand>().AnyAsync(b => b.Id == brandId);
        if (!brandExists) throw new ProductMustHaveBrandException();
    }

    public async Task ProductMustHaveCategory(IEnumerable<int> categoryIds)
    {
        foreach (var categoryId in categoryIds)
        {
            var category = await _unitOfWork.GetReadRepository<Category>().AnyAsync(c => c.Id == categoryId);
            if (!category) throw new ProductMustHaveCategoryException(categoryId);
        }
    }
}
