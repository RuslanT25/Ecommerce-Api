using AutoMapper;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted)
            ?? throw new Exception("The product could not be found.");

        _mapper.Map(request, product);

        var productCategories = await _unitOfWork.GetReadRepository<ProductCategory>()
            .GetAllAsync(x => x.ProductId == request.Id);

        if (productCategories != null && productCategories.Count != 0)
        {
            await _unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);
        }

        foreach (var categoryId in request.CategoryIds)
        {
            await _unitOfWork.GetWriteRepository<ProductCategory>()
                .AddAsync(new ProductCategory
                {
                    CategoryId = categoryId,
                    ProductId = product.Id,
                });
        }

        await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }
}
