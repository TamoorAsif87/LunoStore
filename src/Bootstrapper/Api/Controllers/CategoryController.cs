using Catalog.Categories.Features.CreateCategory;
using Catalog.Categories.Features.DeleteCategory;
using Catalog.Categories.Features.GetAllBooksOfCategory;
using Catalog.Categories.Features.GetAllCategories;
using Catalog.Categories.Features.GetCategoryById;
using Catalog.Categories.Features.UpdateCategory;
using Catalog.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(IMediator mediator) : ControllerBase
{
    // GET: api/Category
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
        return Ok(result);
    }

    // GET: api/Category/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CategoryDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    // POST: api/Category
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Guid>> Create([FromBody] CategoryDto category, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(new CreateCategoryCommand(category), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    // PUT: api/Category/{id}
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CategoryDto category, CancellationToken cancellationToken)
    {
        if (id != category.Id)
            return BadRequest("Mismatched ID");

        await mediator.Send(new UpdateCategoryCommand(category), cancellationToken);
        return NoContent();
    }

    // DELETE: api/Category/{id}
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
        return NoContent();
    }

    // Get: api/Category/books/{categoryId}/{categoryName}
    [HttpGet("books/{categoryId:guid}/{categoryName}")]
    public async Task<IActionResult> CategoryBooks(Guid categoryId, string categoryName, CancellationToken cancellationToken)
    {
        var books =  await mediator.Send(new GetAllBooksOfCategory(categoryId,categoryName), cancellationToken);
        return Ok(books);
    }
}
