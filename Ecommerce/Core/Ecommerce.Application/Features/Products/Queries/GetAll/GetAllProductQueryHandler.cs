using AutoMapper;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Features.Products.Queries.GetAll;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync(
            predicate: x => x.IsDeleted == false,
                 include: query =>
                     query.Include(p => p.Brand)
                          .Include(p => p.ProductCategories)
                              .ThenInclude(pc => pc.Category),
                 enableTracking: false
             );

        var productDtos = _mapper.Map<IList<GetAllProductsQueryResponse>>(products);

        return productDtos;
    }
}
