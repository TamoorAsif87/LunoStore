using Catalog.Contracts.Dtos;
using Catalog.Contracts.Products.Features.GetProductById;
using Catalog.Products.Features.CreateProduct;
using Catalog.Products.Features.DeleteProduct;
using Catalog.Products.Features.GetAllProducts;
using Catalog.Products.Features.GetProductsWithFilter;
using Catalog.Products.Features.UpdateProduct;
using Catalog.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IMediator mediator) : ControllerBase
{
    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Ok(result);
    }

    // GET: api/Product/filter
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Filter([FromQuery] ProductSpecs specs, CancellationToken cancellationToken)
    {
        var filtered = await mediator.Send(new GetProductWithFiltersQuery(specs), cancellationToken);
        return Ok(filtered);
    }


    // GET: api/Product/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    // POST: api/Product
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] ProductDto product, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(new CreateProductCommand(product), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    // PUT: api/Product/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] ProductDto product,
     CancellationToken cancellationToken)
    {
        if (id != product.id)
            return BadRequest("Mismatched ID");

        await mediator.Send(new UpdateProductCommand(product), cancellationToken);
        return NoContent();
    }

    // DELETE: api/Product/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return NoContent();
    }

}

