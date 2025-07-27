namespace Basket.Basket.Dtos;

public record ShoppingCartDto(Guid Id,string UserName,IEnumerable<ShoppingCartItemDto> Items);

