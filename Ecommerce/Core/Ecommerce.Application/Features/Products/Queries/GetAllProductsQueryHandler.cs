using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces.AutoMapper;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Features.Products.Queries;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomMapper _mapper;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork,ICustomMapper customMapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = customMapper;
    }
    public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync(include: x => x.Include(b => b.Brand));

        _mapper.Map<BrandDTO, Brand>(new Brand());

        var responses = _mapper.Map<GetAllProductsQueryResponse, Product>(products);

        foreach (var item in responses)
            item.Price -= (item.Price * item.Discount / 100);

        return responses;
    }
}
