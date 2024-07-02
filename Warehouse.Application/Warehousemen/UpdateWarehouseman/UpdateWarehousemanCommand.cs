using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Warehousemen.UpdateWarehouseman;

public sealed record UpdateWarehousemanCommand() : ICommand<WarehousemanModel>;
