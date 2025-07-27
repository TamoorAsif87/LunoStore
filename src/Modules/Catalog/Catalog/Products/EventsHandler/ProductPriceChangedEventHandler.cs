namespace Catalog.Products.EventsHandler;

public class ProductPriceChangedEventHandler(ILogger<ProductCreatedEventHandler> logger,IBus bus) : INotificationHandler<ProductPriceChangedEvent>
{
    public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Event Handled: ${notification.GetType().Name}");

        var productPriceChangedEvent = new ProductPriceChangedIntegrationEvent
        {
            ProductId = notification.Product.Id,
            Price = notification.Product.Price,
        };

        await bus.Publish(productPriceChangedEvent, cancellationToken);
    }
}
