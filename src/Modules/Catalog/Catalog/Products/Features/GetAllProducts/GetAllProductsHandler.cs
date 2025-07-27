namespace Catalog.Products.Features.GetAllProducts;

public record GetAllProductsQuery() : IQuery<IEnumerable<ProductDto>>;

internal class GetAllProductsQueryHandler(CatalogDbContext context, IMapper mapper)
    : IQueryHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await context.Products.AsNoTracking()
                                        .Include(p => p.Category)
                                        .ToListAsync(cancellationToken);

        return mapper.Map<List<ProductDto>>(products);
    }
}
