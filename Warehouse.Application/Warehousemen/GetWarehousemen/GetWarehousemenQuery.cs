using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Warehousemen.GetWarehousemen;

public sealed record GetWarehousemenQuery : IQuery<IEnumerable<WarehousemanModel>>;