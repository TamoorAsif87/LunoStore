namespace Shared.Models;

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public PaginatedResult(IEnumerable<T> items, int totalCount, int page,int totalPages)
    {
        Items = items;
        TotalCount = totalCount;
        Page = page;
        TotalPages = totalPages;
    }
}