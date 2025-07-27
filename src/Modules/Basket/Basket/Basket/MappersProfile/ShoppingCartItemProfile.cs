namespace Basket.Basket.Mappings;

public class ShoppingCartItemProfile : Profile
{
    public ShoppingCartItemProfile()
    {
        CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
    }
}
