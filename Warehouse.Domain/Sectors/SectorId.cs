using Warehouse.Domain.Shared;

namespace Warehouse.Domain.Sectors;

public sealed record SectorId : EntityId
{
    public SectorId(Guid id) : base(id)
    {
    }

    public SectorId(string id) : base(id)
    {
    }

    public SectorId()
    {
    }
}