namespace Basket.Basket.MappersProfile;

public class BasketMappingProfile : Profile
{
    public BasketMappingProfile()
    {
        // Map CheckoutBasketDto to BasketCheckoutIntegrationEvent
        CreateMap<CheckoutBasketDto, BasketCheckoutIntegrationEvent>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map ItemsDto (record) to ItemsForCheckOutBasketIntegrationEvent
        CreateMap<ItemsDto, ItemsForCheckOutBasketIntegrationEvent>();
    }
}
