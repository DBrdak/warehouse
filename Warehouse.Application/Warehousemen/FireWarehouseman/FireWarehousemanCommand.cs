using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Warehousemen.FireWarehouseman;

public sealed record FireWarehousemanCommand(Guid Id) : ICommand;
