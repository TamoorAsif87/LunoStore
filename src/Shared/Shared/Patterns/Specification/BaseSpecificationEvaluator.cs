namespace Shared.Patterns.Specification;

public static class BaseSpecificationEvaluator<T>
{
    public static IEnumerable<T> Filters(IEnumerable<T> items, ISpecification<T> specs)
    {
        if(specs.Criteria != null)
        {
            items = items.Where(specs.Criteria);
        }

        if (specs.OrderBy != null)
            items = items.OrderBy(specs.OrderBy);

        if (specs.OrderByDescending != null)
            items = items.OrderByDescending(specs.OrderByDescending);

        if (specs.Skip.HasValue)
        {
            items = items.Skip(specs.Skip.Value);
        }

        if (specs.Take.HasValue)
        {
            items = items.Take(specs.Take.Value);
        }

        return items;
    }
}
