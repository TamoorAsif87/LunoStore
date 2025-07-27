namespace Catalog.Categories.Features.GetAllCategories;

public record GetAllCategoriesQuery() : IQuery<IEnumerable<CategoryDto>>;

internal class GetAllCategoriesQueryHandler(CatalogDbContext context,IMapper mapper) : IQueryHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
    {
        return await context.Categories.AsNoTracking()
            .ProjectTo<CategoryDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

