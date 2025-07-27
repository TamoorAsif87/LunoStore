namespace Catalog.Categories.Models;

public class Category:Entity<Guid>
{
    public string Name { get; private set; } = default!;
    public ICollection<Product>? Products { get; set; }

    public static Category Create(Guid id, string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        return new Category { Id = id, Name = name };
    }

    public void Update(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }
}
