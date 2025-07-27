namespace Basket.Basket.Mappings;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
    }
}
