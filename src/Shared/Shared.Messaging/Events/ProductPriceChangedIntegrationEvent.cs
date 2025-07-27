namespace Shared.Messaging.Events;

public class ProductPriceChangedIntegrationEvent:IntegrationEvent
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
}
