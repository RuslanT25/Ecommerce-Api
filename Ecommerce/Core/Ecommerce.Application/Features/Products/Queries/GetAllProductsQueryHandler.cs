using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();
        List<GetAllProductsQueryResponse> responses = new();
        foreach (var product in products)
        {
            responses.Add(new GetAllProductsQueryResponse
            {
                Title = product.Title,
                Description = product.Description,
                Discount = product.Discount,
                Price = product.Price - (product.Discount * product.Discount / 100)
            });
        }

        return responses;
    }
}
