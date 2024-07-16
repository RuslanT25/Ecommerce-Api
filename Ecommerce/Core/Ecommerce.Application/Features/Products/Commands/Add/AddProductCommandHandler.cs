using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.GetAll;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Features.Products.Commands.Add;

public class AddProductCommandHandler : IRequestHandler<AddProductCommandRequest, GetAllProductsQueryResponse>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public AddProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetAllProductsQueryResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
    {
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
