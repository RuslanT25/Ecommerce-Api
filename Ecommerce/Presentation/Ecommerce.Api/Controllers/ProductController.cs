using Ecommerce.Application.Features.Products.Commands.Create;
using Ecommerce.Application.Features.Products.Commands.Delete;
using Ecommerce.Application.Features.Products.Commands.Update;
using Ecommerce.Application.Features.Products.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var response = await _mediator.Send(new GetAllProductsQueryRequest());
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(CreateProductCommandRequest request)
    {
        var product = await _mediator.Send(request);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}
