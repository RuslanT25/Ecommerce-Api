using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.Delete;

public class DeleteProductCommandRequest : IRequest
{
    public int Id { get; set; }
}
