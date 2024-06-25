using Warehouse.Domain.Shared;

namespace Warehouse.Domain.Warehousemen;

public sealed record WarehousemanId : EntityId
{
    public WarehousemanId(Guid id) : base(id)
    {
    }

    public WarehousemanId(string id) : base(id)
    {
    }

    public WarehousemanId()
    {
    }
}