namespace Shared.DDD;

using Microsoft.AspNetCore.Identity;
using System;

public abstract class AggregateIdentityUser : IdentityUser, IAggregate<string>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public DateTime CreatedAt { get ; set ; }
    public DateTime UpdatedAt { get; set;}

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public IDomainEvent[] ClearDomainEvent()
    {
        var dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequeuedEvents;
    }
}
