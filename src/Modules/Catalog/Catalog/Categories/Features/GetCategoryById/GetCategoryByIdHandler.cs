namespace Catalog.Categories.Features.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IQuery<CategoryDto>;

internal class GetCategoryByIdQueryHandler(CatalogDbContext context) : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await context.Categories.FindAsync([query.Id], cancellationToken);
        if (category is null) throw new CategoryNotFoundException("Category",query.Id);

        return new CategoryDto(category.Id, category.Name);
    }
}
