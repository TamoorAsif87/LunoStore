namespace Basket.Basket.Dtos;

public record ItemsDto(
    Guid ProductId,
    decimal Price,
    int Quantity,
    List<string> ProductImages,
    string ProductName,
    List<string> ProductColors
    );
