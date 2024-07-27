using AutoMapper;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ecommerce.Application.Bases;

public class BaseHandler
{
    public readonly IMapper _mapper;
    public readonly IUnitOfWork _unitOfWork;
    public readonly IHttpContextAccessor _httpContextAccessor;
    public readonly string userId;

    public BaseHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
