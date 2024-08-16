using Warehouse.Domain.Shared;

namespace Warehouse.Domain.Drivers;

public sealed record DriverId : EntityId
{
    public DriverId(Guid id) : base(id)
    {
    }

    public DriverId(string id) : base(id)
    {

    }

    public DriverId()
    {
    }
}