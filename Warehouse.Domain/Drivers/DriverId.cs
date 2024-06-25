using Warehouse.Domain.Shared;

namespace Warehouse.Domain.Drivers;

public sealed record DriverId : EntityId
{
    public DriverId(int id) : base(id)
    {
    }

    public DriverId()
    {
    }
}