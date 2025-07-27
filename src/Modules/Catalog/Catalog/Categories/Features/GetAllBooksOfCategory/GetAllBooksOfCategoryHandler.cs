namespace Catalog.Categories.Features.GetAllBooksOfCategory;

public record GetAllBooksOfCategory(Guid Id,string CategoryName) : IQuery<IEnumerable<ProductDto>>;

internal class GetAllBooksOfCategoryQueryHandler(CatalogDbContext context, IMapper mapper) : IQueryHandler<GetAllBooksOfCategory, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllBooksOfCategory query, CancellationToken cancellationToken)
    {
        var products = await context.Products.AsNoTracking()
     .                                        Where(p => p.CategoryId == query.Id ||
                                            (!string.IsNullOrEmpty(query.CategoryName) && p.Category!.Name == query.CategoryName))
                                            .ToListAsync(cancellationToken);

        if (products.Count == 0)
            return Enumerable.Empty<ProductDto>();

        return mapper.Map<IEnumerable<ProductDto>>(products);
    }
}

