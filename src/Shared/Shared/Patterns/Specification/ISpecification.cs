namespace Shared.Patterns.Specification;

public interface ISpecification<T>
{
    public Func<T,bool> Criteria { get;}

    Func<T, object>? OrderBy { get; }
    Func<T, object>? OrderByDescending { get; }

    int? Skip { get; }
    int? Take { get; }
}
