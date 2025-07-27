using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.Features.CreateOrder;
using Ordering.Orders.Dtos;
using Ordering.Orders.Features.DeleteOrder;
using Ordering.Orders.Features.GetAllOrders;
using Ordering.Orders.Features.GetOrderById;
using Ordering.Orders.Features.GetOrdersByCustomer;
using Ordering.Orders.Features.UpdateOrder;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class OrderController(ISender sender) : ControllerBase
{
    // GET: api/order
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await sender.Send(new GetAllOrdersQuery());
        return Ok(result);
    }

    // GET: api/order/customer/{customerId}
    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetOrdersByCustomerId(string customerId)
    {
        var result = await sender.Send(new GetOrdersByCustomerIdQuery(customerId));
        return Ok(result);
    }

    // GET: api/order/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var result = await sender.Send(new GetOrderByIdQuery(id));
        return Ok(result);
    }

    // POST: api/order
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
    {
        var orderId = await sender.Send(new CreateOrderCommand(order));
        return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, new { id = orderId });
    }

    // PUT: api/order
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderDto order)
    {
        await sender.Send(new UpdateOrderCommand(order));
        return NoContent();
    }

    // DELETE: api/order/{id}
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        await sender.Send(new DeleteOrderCommand(id));
        return NoContent();
    }
}

