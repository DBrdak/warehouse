using Warehouse.Domain.Shared;

namespace Warehouse.Domain.Freights;

public sealed record FreightId : EntityId
{
    public FreightId(Guid id) : base(id)
    {
    }

    public FreightId(string id) : base(id)
    {
    }

    public FreightId()
    {
    }
}