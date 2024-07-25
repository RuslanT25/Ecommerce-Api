using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.Delete;

public class DeleteProductCommandRequest : IRequest<Unit>
{
    public int Id { get; set; }
}
