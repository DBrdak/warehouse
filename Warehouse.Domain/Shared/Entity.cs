using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Domain.Shared;

public class Entity<TEntityId> : IEntity where TEntityId : EntityId, new()
{
    public TEntityId Id { get; protected set; }

    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(TEntityId? id)
    {
        Id = id ?? new TEntityId();
        _domainEvents = new();
    }

    [System.Text.Json.Serialization.JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    private Entity(TEntityId id, List<IDomainEvent> domainEvents)
    {
        Id = id;
        _domainEvents = domainEvents;
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents() =>
        _domainEvents;

    public void ClearDomainEvents() =>
        _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}