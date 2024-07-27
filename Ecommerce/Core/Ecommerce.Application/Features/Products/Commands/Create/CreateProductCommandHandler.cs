using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.Features.Products.Queries.GetAll;
using Ecommerce.Application.Features.Products.Rules;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Features.Products.Commands.Create;

public class CreateProductCommandHandler : BaseHandler, IRequestHandler<CreateProductCommandRequest, GetAllProductsQueryResponse>
{
    private readonly ProductRules _productRules;

    public CreateProductCommandHandler(ProductRules productRules, IMapper _mapper, IUnitOfWork _unitOfWork, IHttpContextAccessor httpContextAccessor)
        : base(_mapper, _unitOfWork, httpContextAccessor)
    {
        _productRules = productRules;
    }

    public async Task<GetAllProductsQueryResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        IList<Product> products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();

        await _productRules.ProductTitleMustNotBeSame(products, request.Title);

        await _productRules.ProductMustHaveBrand(request.BrandId);

        await _productRules.ProductMustHaveCategory(request.CategoryIds);

        Product newProduct = new(request.Title, request.Description, request.Price, request.Discount, request.BrandId);

        await _unitOfWork.GetWriteRepository<Product>().AddAsync(newProduct);
        if (await _unitOfWork.SaveChangesAsync() > 0)
        {
            foreach (var categoryId in request.CategoryIds) 
            {
                await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
                {
                    ProductId = newProduct.Id,
                    CategoryId = categoryId,
                });
            }

            await _unitOfWork.SaveChangesAsync();
        }

        var product = await _unitOfWork.GetReadRepository<Product>().GetAsync
            (x => x.Id == newProduct.Id,
            include: x => x
            .Include(b => b.Brand)
            .Include(x => x.ProductCategories)
            .ThenInclude(x => x.Category)) ?? throw new Exception("The product could not be found.");

        var productDto = _mapper.Map<GetAllProductsQueryResponse>(product);
        return productDto;
    }
}
