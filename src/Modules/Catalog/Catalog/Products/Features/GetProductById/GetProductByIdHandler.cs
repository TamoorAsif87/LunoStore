namespace Catalog.Products.Features.GetProductById;


public class GetProductByIdQueryHandler(CatalogDbContext context, IMapper mapper)
    : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync([query.Id], cancellationToken);
        return product is null ?
            throw new ProductNotFoundException("Product", query.Id)
            : mapper.Map<ProductDto>(product);
    }
}
