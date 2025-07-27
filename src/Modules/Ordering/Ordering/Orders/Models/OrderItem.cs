namespace Ordering.Orders.Models;

public class OrderItem:Entity<Guid>
{
    public Guid ProductId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public Guid OrderId { get; private set; }
    public List<string> ProductImages { get; private set; } = default!;
    public string ProductName { get; private set; } = default!;
    public List<string> ProductColors { get; private set; } = default!;

    internal OrderItem(Guid productId,Guid orderId,decimal price,int quantity,List<string> productImages,List<string> productColors, string productName)
    {
        ProductId = productId;
        OrderId = orderId;
        Price = price;
        Quantity = quantity;
        ProductImages = productImages;
        ProductColors = productColors;
        ProductName = productName;

    }
}
