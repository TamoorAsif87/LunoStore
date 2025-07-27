namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<bool>;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
    }
}


public class DeleteBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, bool>
{
    public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        return await repository.DeleteBasket(request.UserName, cancellationToken);
    }
}

