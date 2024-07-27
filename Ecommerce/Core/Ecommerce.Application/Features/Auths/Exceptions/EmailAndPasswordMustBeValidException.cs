using Ecommerce.Application.Bases;

namespace Ecommerce.Application.Features.Auths.Exceptions;

public class EmailAndPasswordMustBeValidException : BaseException
{
    public EmailAndPasswordMustBeValidException() : base("Incorrect credentials.") { }
}
