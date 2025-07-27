namespace Ordering.Orders.Dtos;

public record PaymentDto(
     string? CardName,
     string CardNumber,
     DateOnly Expiration,
     string CVV 
    );

