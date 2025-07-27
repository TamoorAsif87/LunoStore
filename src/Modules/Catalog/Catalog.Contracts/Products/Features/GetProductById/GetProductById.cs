using Catalog.Contracts.Dtos;
using Shared.Contracts.CQRS;

namespace Catalog.Contracts.Products.Features.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductDto>;