namespace Shared.Patterns.Specification;

public class BaseSpecification<T>(Func<T, bool> criteria) : ISpecification<T>
{
    public BaseSpecification():this(null)
    {
        
    }

    public int? Skip { get; private set; }
    public int? Take { get; private set; }

    public Func<T, bool> Criteria => criteria;

    public Func<T, object>? OrderBy { get; protected set; }
    public Func<T, object>? OrderByDescending { get; protected set; }

    protected void AddOrderBy(Func<T, object> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Func<T, object> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }

    
}
