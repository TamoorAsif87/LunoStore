using Ordering.Orders.Events;

namespace Ordering.Orders.EventsHandler;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreateEvent>
{
    public Task Handle(OrderCreateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Domain Event Handled: {notification.GetType().Name}");
        return Task.CompletedTask;
    }
}
