using Ecommerce.Application.Bases;

namespace Ecommerce.Application.Features.Auths.Exceptions;

public class EmailAddressShouldBeValidException : BaseException
{
    public EmailAddressShouldBeValidException() : base("Invalid email.") { }
}
