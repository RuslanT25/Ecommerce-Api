using Ecommerce.Application.Interfaces.Tokens;
using Ecommerce.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email)
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
        var random = new byte[64];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(random);

        return Convert.ToBase64String(random);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        TokenValidationParameters tokenValidationParamaters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecretKey)),
            ValidateLifetime = false
        };

        JwtSecurityTokenHandler tokenHandler = new();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParamaters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg
            .Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Token not found.");

        return principal;
    }
}
