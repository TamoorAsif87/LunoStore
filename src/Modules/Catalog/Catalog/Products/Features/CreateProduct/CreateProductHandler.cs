using Shared.Services.Contracts;

namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<Guid>;

public class ProductCreateCommandValidator : AbstractValidator<CreateProductCommand>
{
    public ProductCreateCommandValidator(CatalogDbContext dbContext)
    {

        RuleFor(x => x.Product.productName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");

        RuleFor(x => x.Product.description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.Product.price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Product.inStock)
            .GreaterThanOrEqualTo(0).WithMessage("InStock must be zero or more.");

    

        RuleFor(x => x.Product.productColors)
            .NotNull().WithMessage("Product colors must be provided.");

        RuleFor(x => x.Product.categoryId)
            .NotEmpty().WithMessage("Category ID is required.")
            .MustAsync(async (categoryId, cancellationToken) =>
                await dbContext.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken))
            .WithMessage("Category with given ID does not exist.");
    }
}



internal class CreateProductCommandHandler(CatalogDbContext context,IFileUpload fileUpload) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var p = command.Product;

        if(command.Product.Files != null && command.Product.Files.Length >0)
        {
            foreach(var file in command.Product.Files)
            {
                var imageUrl = await fileUpload.UploadImageAsync(file);
                p.productImages.Add(imageUrl);
            }
        }

        var product = Product.Create(p.id, p.productName, p.description, p.price, p.inStock, p.productImages, p.productColors, p.categoryId);

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}
