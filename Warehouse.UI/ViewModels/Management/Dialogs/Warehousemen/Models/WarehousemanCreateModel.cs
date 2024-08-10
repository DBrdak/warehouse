namespace Warehouse.UI.ViewModels.Management.Dialogs.Warehousemen.Models;

internal sealed record WarehousemanCreateModel(int IdentificationNumber, string FirstName, string LastName, string? Position, int SectorNumber)
{
    public static WarehousemanCreateModel Init() =>
        new(
            0,
            string.Empty,
            string.Empty,
            string.Empty,
            1);
}