namespace Catalog.Categories.Features.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : ICommand;

internal class DeleteCategoryCommandHandler(CatalogDbContext context) : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await context.Categories.FindAsync([command.Id], cancellationToken);
        if (category is null) throw new CategoryNotFoundException("Category",command.Id);

        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}