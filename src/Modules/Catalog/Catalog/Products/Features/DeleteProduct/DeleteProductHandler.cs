namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand;

internal class DeleteProductCommandHandler(CatalogDbContext context) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync([command.Id], cancellationToken);
        if (product is null) throw new ProductNotFoundException("Product", command.Id);

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
