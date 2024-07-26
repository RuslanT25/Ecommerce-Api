using Ecommerce.Application.Interfaces.Tokens;
using Ecommerce.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

    public async Task<JwtSecurityToken> CreateToken(AppUser user)
    {
        var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

        var userRoles = await _userManager.GetRolesAsync(user);
        claims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecretKey));

        var token = new JwtSecurityToken(
            issuer: _tokenOption.Issuer,
            audience: _tokenOption.Audience,
            expires: DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

        await _userManager.AddClaimsAsync(user, claims);

        return token;
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
