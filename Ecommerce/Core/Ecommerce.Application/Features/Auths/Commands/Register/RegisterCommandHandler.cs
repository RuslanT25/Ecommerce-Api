using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Auths.Commands.Register;

public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, Unit>
{
    public RegisterCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        : base(mapper, unitOfWork, httpContextAccessor)
    {
        
    }
    public Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
