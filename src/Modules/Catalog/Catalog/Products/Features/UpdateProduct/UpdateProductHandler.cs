using Shared.Services.Contracts;

namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDto Product) : ICommand;


public class ProductUpdateCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public ProductUpdateCommandValidator(CatalogDbContext dbContext)
    {
        RuleFor(x => x.Product.id)
            .NotEmpty().WithMessage("Product ID is required.")
            .MustAsync(async (id, ct) =>
                await dbContext.Products.AnyAsync(p => p.Id == id, ct))
            .WithMessage("Product with given ID does not exist.");

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
            .MustAsync(async (categoryId, ct) =>
                await dbContext.Categories.AnyAsync(c => c.Id == categoryId, ct))
            .WithMessage("Category with given ID does not exist.");
    }
}


internal class UpdateProductCommandHandler(CatalogDbContext context,IFileUpload fileUpload) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var p = command.Product;
        var product = await context.Products.FindAsync([p.id], cancellationToken);
        if (product is null) throw new KeyNotFoundException($"Product {p.id} not found.");
        
        if(command.Product.Files != null && command.Product.Files.Length > 0)
        {
            foreach (var file in command.Product.Files)
            {
                var imageUrl = await fileUpload.UploadImageAsync(file);
                p.productImages.Add(imageUrl);
            }
        }

        product.Update(p.productName, p.description, p.price, p.inStock, p.productImages, p.productColors, p.categoryId);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
