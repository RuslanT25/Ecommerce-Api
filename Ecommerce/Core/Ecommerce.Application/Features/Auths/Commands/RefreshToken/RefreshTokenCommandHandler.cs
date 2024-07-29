using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.Features.Auths.Rules;
using Ecommerce.Application.Interfaces.Tokens;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ecommerce.Application.Features.Auths.Commands.RefreshToken;

public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    public RefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, ITokenService tokenService) : base(mapper, unitOfWork, httpContextAccessor)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        string email = principal!.FindFirstValue(ClaimTypes.Email)!;

        AppUser? user = await _userManager.FindByEmailAsync(email);

        await AuthRules.RefreshTokenShouldNotBeExpired(user!.RefreshTokenExpiryTime);

        JwtSecurityToken newAccessToken = await _tokenService.CreateToken(user);
        string newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
        };
    }
}
