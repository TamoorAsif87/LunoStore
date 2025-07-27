using Shared.Models;

namespace Catalog.Products.Features.GetProductsWithFilter;

public record GetProductWithFiltersQuery(ProductSpecs Specs) : IQuery<PaginatedResult<ProductDto>>;

internal class GetProductWithFiltersHandler(
    CatalogDbContext context,
    IMapper mapper
) : IQueryHandler<GetProductWithFiltersQuery, PaginatedResult<ProductDto>>
{
    public async Task<PaginatedResult<ProductDto>> Handle(GetProductWithFiltersQuery query, CancellationToken cancellationToken)
    {
        var specification = new ProductSpecification(query.Specs);

        query.Specs.isPaginationEnabled = true;

        var specificationWithPagination = new ProductSpecification(query.Specs);

        var allProducts = await context.Products.AsNoTracking().ToListAsync(cancellationToken);

        var productsFilteredWithoutPagination = BaseSpecificationEvaluator<Product>.Filters(allProducts, specification).ToList();

        var productsFilteredWithPagination = BaseSpecificationEvaluator<Product>.Filters(allProducts, specificationWithPagination).ToList();

        var result = mapper.Map<IEnumerable<ProductDto>>(productsFilteredWithPagination);

        var totalCount = productsFilteredWithoutPagination.Count();

        var totalPages = (int)Math.Ceiling(totalCount / (double)query.Specs.PageSize);

        return new PaginatedResult<ProductDto>(result, totalCount,query.Specs.Page,totalPages);
    }
}