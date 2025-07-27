namespace Catalog.Products.MappersProfile;


public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        // Entity -> DTO
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.inStock, opt => opt.MapFrom(src => src.InStock))
            .ForMember(dest => dest.productImages, opt => opt.MapFrom(src => src.ProductImages))
            .ForMember(dest => dest.productColors, opt => opt.MapFrom(src => src.ProductColors))
            .ForMember(dest => dest.Files, opt => opt.Ignore())
            .ForMember(dest => dest.categoryId, opt => opt.MapFrom(src => src.CategoryId));

        // DTO -> Entity
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.productName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
            .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.inStock))
            .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.productImages))
            .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.productColors))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.categoryId))
            .ForMember(dest => dest.Category, opt => opt.Ignore())             
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForSourceMember(src => src.Files, opt => opt.DoNotValidate());       
    }
}
