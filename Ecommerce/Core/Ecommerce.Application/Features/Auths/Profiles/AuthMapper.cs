using AutoMapper;
using Ecommerce.Application.Features.Auths.Commands.Register;
using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Application.Features.Auths.Profiles;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<AppUser, RegisterCommandRequest>().ReverseMap();
    }
}
