using Warehouse.Domain.Shared;

namespace Warehouse.Domain.Clients;

public sealed record ClientId : EntityId
{
    public ClientId(Guid id) : base(id)
    {
    }

    public ClientId(string id) : base(id)
    {
    }

    public ClientId()
    {
    }
}