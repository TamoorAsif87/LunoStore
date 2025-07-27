namespace Basket.Basket.Features.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<ShoppingCartDto>;


public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
{
    public GetBasketQueryValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
    }
}

public class GetBasketQueryHandler(IBasketRepository repository, IMapper mapper)
    : IQueryHandler<GetBasketQuery, ShoppingCartDto>
{
    public async Task<ShoppingCartDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasket(request.UserName,true, cancellationToken);
        return mapper.Map<ShoppingCartDto>(cart);
    }
}
