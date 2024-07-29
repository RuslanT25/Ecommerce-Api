using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.Features.Auths.Rules;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Commands.Revoke;

public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommandRequest, Unit>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AuthRules _authRules;

    public RevokeCommandHandler(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AuthRules authRules) : base(mapper, unitOfWork, httpContextAccessor)
    {
        _userManager = userManager;
        _authRules = authRules;
    }

    public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByEmailAsync(request.Email);
        await _authRules.EmailAddressShouldBeValid(user);

        user!.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _userManager.UpdateAsync(user);

        return Unit.Value;
    }
}
