namespace Ordering.Orders.Dtos;

 public record OrderItemDto(
    Guid ProductId,
    decimal Price,
    int Quantity, 
    Guid OrderId, 
    List<string> ProductImages,
    string ProductName,
    List<string> ProductColors
    );
