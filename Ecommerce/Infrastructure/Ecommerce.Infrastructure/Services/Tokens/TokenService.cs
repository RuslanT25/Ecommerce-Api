using Ecommerce.Application.Interfaces.Tokens;
using Ecommerce.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ecommerce.Infrastructure.Services.Tokens;
public class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenOption _tokenOption;

    public TokenService(IOptions<TokenOption> options, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _tokenOption = options.Value;
    }

    public Task<JwtSecurityToken> CreateToken(AppUser user, IList<string> roles)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken()
    {
        throw new NotImplementedException();
    }
}
