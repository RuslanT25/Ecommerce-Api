using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.Features.Auths.Rules;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Ecommerce.Application.Features.Auths.Commands.Register;

public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, Unit>
{
    private readonly AuthRules _authRules;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    public RegisterCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AuthRules authRules, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        : base(mapper, unitOfWork, httpContextAccessor)
    {
        _authRules = authRules;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var hasUser = await _userManager.FindByEmailAsync(request.Email);
        await AuthRules.UserShouldBeExists(hasUser);

        var user = _mapper.Map<RegisterCommandRequest, AppUser>(request);
        IdentityResult result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("user"))
                await _roleManager.CreateAsync(new AppRole
                {
                    Id = Guid.NewGuid(),
                    Name = "user",
                    NormalizedName = "USER",
                });

            await _userManager.AddToRoleAsync(user, "user");
        }

        return Unit.Value;
    }
}
