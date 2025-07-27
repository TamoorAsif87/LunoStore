namespace Basket.Basket.Dtos;

public record CheckoutBasketDto(
    string UserName,
    decimal TotalPrice,
    string? CustomerId,
    List<ItemsDto> Items,
    
     // Shipping Address and Billing Address
     string FirstName,
     string LastName,
     string EmailAddress,
     string AddressLine,
     string Country,
     string ZipCode,
     string City,
     string State,

    // Payment
    string? CardName,
    string CardNumber,
    DateOnly Expiration,
    string Cvv
);
