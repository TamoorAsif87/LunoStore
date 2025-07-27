using Basket.Basket.Features.AddItemToBasket;
using Basket.Basket.Features.CheckoutBasket;
using Basket.Basket.Features.CreateBasket;
using Basket.Basket.Features.DecreaseItemQuantity;
using Basket.Basket.Features.DeleteBasket;
using Basket.Basket.Features.GetBasket;
using Basket.Basket.Features.IncreaseItemQuantity;
using Basket.Basket.Features.RemoveItemFromBasket;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController(IMediator mediator) : ControllerBase
{
    [HttpGet("{userName}")]
    public async Task<IActionResult> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBasketQuery(userName), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBasket([FromBody] CreateBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteBasketCommand(userName), cancellationToken);
        return NoContent();
    }

    [HttpPost("{userName}/items")]
    public async Task<IActionResult> AddItem([FromRoute] string userName, [FromBody] AddItemCommand command, CancellationToken cancellationToken)
    {
        if (userName != command.UserName) return BadRequest("Username mismatch.");
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{userName}/items/{productId}")]
    public async Task<IActionResult> RemoveItem(string userName, Guid productId, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoveItemCommand(userName, productId), cancellationToken);
        return NoContent();
    }

    [HttpPut("{userName}/items/{productId}/increase")]
    public async Task<IActionResult> IncreaseItemQuantity(string userName, Guid productId, int quantity, CancellationToken cancellationToken)
    {
        await mediator.Send(new IncreaseItemQuantityCommand(userName, productId, quantity), cancellationToken);
        return Ok();
    }

    [HttpPut("{userName}/items/{productId}/decrease")]
    public async Task<IActionResult> DecreaseItemQuantity(string userName, Guid productId, [FromQuery] int quantity, CancellationToken cancellationToken)
    {
        await mediator.Send(new DecreaseItemQuantityCommand(userName, productId, quantity), cancellationToken);
        return Ok();
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> CheckoutBasket(    
    [FromBody] CheckOutBasketCommand command,
    CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (string.IsNullOrEmpty(result))
        {
            return BadRequest("Checkout failed.");
        }

        return Ok(new { orderId = result });
    }
}
