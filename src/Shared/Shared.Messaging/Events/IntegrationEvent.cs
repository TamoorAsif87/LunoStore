namespace Shared.Messaging.Events;

public class IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.UtcNow;
   public string EventType => GetType().AssemblyQualifiedName!;
}
