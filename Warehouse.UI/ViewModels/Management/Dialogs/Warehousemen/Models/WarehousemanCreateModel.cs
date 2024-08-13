using Warehouse.Application.Warehousemen.Models;

namespace Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen.Models;

internal sealed record WarehousemanCreateModel(string IdentificationNumber, string FirstName, string LastName, string? Position, int? SectorNumber)
{
    public static WarehousemanCreateModel Init() =>
        new(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            null);

    public static WarehousemanCreateModel FromExisting(WarehousemanModel warehouseman) =>
        new(
            warehouseman.IdentificationNumber.ToString(),
            warehouseman.FirstName,
            warehouseman.LastName,
            warehouseman.Position,
            warehouseman.Sector?.Number);
}