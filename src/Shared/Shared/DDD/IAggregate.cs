namespace Shared.DDD;

public interface IAggregate<T>:IAggregate, IEntity<T> {}

public interface IAggregate:IEntity
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvent();
}
