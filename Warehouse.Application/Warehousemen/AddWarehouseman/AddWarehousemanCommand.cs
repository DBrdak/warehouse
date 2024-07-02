using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Warehousemen.AddWarehouseman;

public sealed record AddWarehousemanCommand() : ICommand<WarehousemanModel>;
