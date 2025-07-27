using MediatR;

namespace Shared.Contracts.CQRS;

public interface ICommand : ICommand<Unit> 
{
}

public interface ICommand<out TResposne>:IRequest<TResposne> where TResposne : notnull
{
}
