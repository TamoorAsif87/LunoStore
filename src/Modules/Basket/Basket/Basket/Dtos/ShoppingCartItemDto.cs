namespace Basket.Basket.Dtos;

public record ShoppingCartItemDto(Guid Id ,Guid ProductId,int Quantity, decimal Price, string ProductName, List<string> ProductColors,List<string> ProductImages, Guid ShoppingCartId = default);