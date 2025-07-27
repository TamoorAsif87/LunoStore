namespace Ordering.Orders.Dtos;

public record OrderDto(
    Guid OrderId,
    List<OrderItemDto> Items,
    string? CustomerId, 
    AddressDto ShippingAddress,  
    AddressDto BillingsAddress,  
    PaymentDto Payment  
    );
