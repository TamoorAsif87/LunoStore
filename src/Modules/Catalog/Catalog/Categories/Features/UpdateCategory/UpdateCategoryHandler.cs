namespace Catalog.Categories.Features.UpdateCategory;

public record UpdateCategoryCommand(CategoryDto Category) : ICommand;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Category).NotNull();

        RuleFor(x => x.Category.Id)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.Category.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
    }
}


internal class UpdateCategoryCommandHandler(CatalogDbContext context) : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Unit> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await context.Categories.FindAsync([command.Category.Id], cancellationToken);
        if (category is null) throw new CategoryNotFoundException("Category",command.Category.Id);

        category.Update(command.Category.Name);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
