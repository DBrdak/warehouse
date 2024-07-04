using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Warehousemen.UpdateWarehouseman;

public sealed record UpdateWarehousemanCommand(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? Position,
    int? SectorNumber) : ICommand<WarehousemanModel>
{
    public UpdateWarehousemanCommand() : this(Guid.Empty, null, null, null, null)
    {

    }
}
