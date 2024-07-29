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

    public Task EmailAndPasswordMustBeValid(AppUser? user,bool checkPassword)
    {
        if (user == null || !checkPassword) throw new EmailAndPasswordMustBeValidException();

        return Task.CompletedTask;
    }
    public Task RefreshTokenShouldNotBeExpired(DateTime? expiryDate)
    {
        if (expiryDate <= DateTime.Now) throw new RefreshTokenShouldNotBeExpiredException();
        return Task.CompletedTask;
    }
}
