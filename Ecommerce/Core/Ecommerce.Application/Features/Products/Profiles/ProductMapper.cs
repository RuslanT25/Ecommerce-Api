using AutoMapper;
using Ecommerce.Application.Features.Products.Commands.Update;
using Ecommerce.Application.Features.Products.DTOs;
using Ecommerce.Application.Features.Products.Queries.GetAll;
using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Application.Features.Products.Profiles;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, GetAllProductsQueryResponse>()
               .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category).ToList()));

        CreateMap<Brand, BrandDTO>();
        CreateMap<Category, CategoryDTO>();

        CreateMap<UpdateProductCommandRequest, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductCategories, opt => opt.Ignore())
                .ForMember(dest => dest.Brand, opt => opt.Ignore());
    }
}
