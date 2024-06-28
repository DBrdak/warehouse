using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Domain.Shared;

public interface IEntity
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();

    void ClearDomainEvents();
}