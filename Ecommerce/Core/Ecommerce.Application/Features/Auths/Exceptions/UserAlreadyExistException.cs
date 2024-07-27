using Ecommerce.Application.Bases;

namespace Ecommerce.Application.Features.Auths.Exceptions;

public class UserAlreadyExistException : BaseException
{
    public UserAlreadyExistException() : base("User already exits.") { }
}
