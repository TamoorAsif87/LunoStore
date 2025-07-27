namespace Catalog.Categories.Features.CreateCategory;

public record CreateCategoryCommand(CategoryDto Category):ICommand<Guid>;


public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
    }
}

internal class CreateCategoryCommandHandler(CatalogDbContext context) : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = Category.Create(command.Category.Id, command.Category.Name);
        context.Add(category);
        await context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
