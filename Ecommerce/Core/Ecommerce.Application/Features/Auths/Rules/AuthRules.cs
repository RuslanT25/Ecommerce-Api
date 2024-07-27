using Ecommerce.Application.Bases;
using Ecommerce.Application.Features.Auths.Exceptions;
using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Application.Features.Auths.Rules;

public class AuthRules : BaseRules
{
    public Task UserShouldBeExists(AppUser? user)
    {
        if (user != null) throw new UserAlreadyExistException();

        return Task.CompletedTask;
    }
}
