
using Microsoft.AspNetCore.Http;

namespace Catalog.Contracts.Dtos;
public record ProductDto(
    Guid id,
    string productName,
    string description,
    decimal price,
    int inStock,
    Guid categoryId,
    List<string>? productColors = null,
    List<string>? productImages = null,
    IFormFile[]? Files = null,
    CategoryDto? Category = null
)
{
    public List<string> productImages { get; init; } = productImages ?? new List<string>();
    public List<string> productColors { get; init; } = productColors ?? new List<string>();
}
