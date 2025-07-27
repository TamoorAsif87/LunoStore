namespace Shared.Messaging.Events;

public class BasketCheckoutIntegrationEvent:IntegrationEvent
{
    public decimal TotalPrice {  get; set; }
    public string? CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public List<ItemsForCheckOutBasketIntegrationEvent> Items { get; set; } = default!;

     // Shipping Address and Billing Address
     public string FirstName { get; set; } = default!;
     public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;   
     public string AddressLine {  get; set; } =default!;
     public string Country { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
     public string City { get; set; } = default!;
     public string State { get; set; } = default!;

    // Payment
    public string? CardName { get; set; }
    public string CardNumber { get; set; } = default!;
    public DateOnly Expiration { get; set; } = default!;
    public string Cvv { get; set; } = default!;
}

public class ItemsForCheckOutBasketIntegrationEvent
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public List<string> ProductImages { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public List<string> ProductColors { get; set; } = default!;
}