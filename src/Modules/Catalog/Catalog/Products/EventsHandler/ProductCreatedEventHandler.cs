using Catalog.Products.Events;
using Microsoft.Extensions.Logging;

namespace Catalog.Products.EventsHandler;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) : INotificationHandler<ProductCreatedEvent>
{
    public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Event Handled: ${notification.GetType().Name}");

        await Task.CompletedTask;
    }
}
