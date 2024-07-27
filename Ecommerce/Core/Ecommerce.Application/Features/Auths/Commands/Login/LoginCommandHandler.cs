using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.Features.Auths.Rules;
using Ecommerce.Application.Interfaces.Tokens;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce.Application.Features.Auths.Commands.Login;

public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AuthRules _authRules;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    public LoginCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, AuthRules authRules, ITokenService tokenService, IConfiguration configuration) : base(mapper, unitOfWork, httpContextAccessor)
    {
        _userManager = userManager;
        _authRules = authRules;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        bool checkPassword = await _userManager.CheckPasswordAsync(user!, request.Password);

        await _authRules.EmailAndPasswordMustBeValid(user, checkPassword);

        IList<string> roles = await _userManager.GetRolesAsync(user!);

        var token = await _tokenService.CreateToken(user);
        string refreshToken = _tokenService.GenerateRefreshToken();

        _ = int.TryParse(_configuration["TokenOption:RefreshTokenExpiration"], out int refreshTokenValidityInDays);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

        await _userManager.UpdateAsync(user);
        await _userManager.UpdateSecurityStampAsync(user);

        string _token = new JwtSecurityTokenHandler().WriteToken(token);

        await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

        return new()
        {
            AccessToken = _token,
            RefreshToken = refreshToken,
            AccessTokenExpiration = token.ValidTo
        };
    }
}
