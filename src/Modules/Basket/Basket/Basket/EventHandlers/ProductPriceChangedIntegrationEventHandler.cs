using Basket.Basket.Features.UpdateProductItemPrice;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Basket.Basket.EventHandlers;

public class ProductPriceChangedIntegrationEventHandler(ILogger<ProductPriceChangedIntegrationEventHandler> logger, IMediator mediator) : IConsumer<ProductPriceChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        logger.LogInformation($"Integration Event Handled: {context.Message.GetType().Name}");

        var result = await mediator.Send(new UpdateProductItemPriceCommand(context.Message.ProductId, context.Message.Price));

        if(result)
        {
            logger.LogInformation($"Product price updated successfully for ProductId: {context.Message.ProductId}");
        }
        else
        {
            logger.LogError($"Failed to update product price for ProductId: {context.Message.ProductId}");
        }
    }
}
