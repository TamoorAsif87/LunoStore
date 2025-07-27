using MassTransit;
using Ordering.Features.CreateOrder;
using Shared.Messaging.Events;

namespace Ordering.Orders.EventsHandler;

public class CheckoutBasketIntegrationEventHandler(ISender sender, ILogger<CheckoutBasketIntegrationEventHandler> logger) : IConsumer<BasketCheckoutIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var createOrderCommand = MapToCreateOrderCommand(context.Message);
        await sender.Send(createOrderCommand);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutIntegrationEvent message)
    {
        // Create address object (used for both shipping and billing)
        var addressDto = new AddressDto(
            message.FirstName,
            message.LastName,
            message.EmailAddress,
            message.AddressLine,
            message.Country,
            message.ZipCode,
            message.City,
            message.State
        );

        // Create payment object
        var paymentDto = new PaymentDto(
            message.CardName,
            message.CardNumber,
            message.Expiration,
            message.Cvv
        );

        // Map items
        var items = message.Items.Select(item => new OrderItemDto(
            item.ProductId,
            item.Price,
            item.Quantity,
            Guid.Empty, // OrderId will be assigned later in the domain
            item.ProductImages,
            item.ProductName,
            item.ProductColors
        )).ToList();

        // Create OrderDto
        var orderDto = new OrderDto(
            message.OrderId,
            items,
            message.CustomerId,
            addressDto,
            addressDto, // Assuming billing and shipping are the same
            paymentDto
        );

        return new CreateOrderCommand(orderDto);
    }
}
