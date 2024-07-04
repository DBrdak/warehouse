using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Warehousemen.HireWarehouseman;

public sealed record HireWarehousemanCommand(int IdentificationNumber, string FirstName, string LastName, string? Position, int SectorNumber) : ICommand<WarehousemanModel>;
