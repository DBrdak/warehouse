using Warehouse.Domain.Shared;

namespace Warehouse.Domain.Transports;

public sealed record TransportId : EntityId
{
    public TransportId(Guid id) : base(id)
    {
    }

    public TransportId(string id) : base(id)
    {
    }

    public TransportId()
    {
    }
}