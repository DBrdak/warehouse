using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Warehousemen.RemoveWarehouseman;

public sealed record RemoveWarehousemanCommand() : ICommand;
