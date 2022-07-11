using MediatorExample.CustomMediator;
using MediatorExample.Models.Product;
using Microsoft.AspNetCore.Mvc;
namespace MediatorExample.Controllers.Product;

[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("api/product/getproducts")]
    public Task<GetProductsResponse> GetProducts(CancellationToken cancellationToken)
    {
        return _mediator.SendAsync(new GetProductsRequest(),cancellationToken);       
    }
}

