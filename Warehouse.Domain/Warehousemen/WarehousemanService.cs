using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Warehousemen;

public sealed class WarehousemanService
{
    public static Result<Warehouseman> HireWarehouseman(
        int identificationNumber,
        string firstName,
        string lastName,
        string? position,
        Sector sector)
    {
        var warehousemanCreateResult = Warehouseman.Create(
            identificationNumber,
            firstName,
            lastName,
            position,
            sector);

        if (warehousemanCreateResult.IsFailure)
        {
            return warehousemanCreateResult.Error;
        }

        var warehouseman = warehousemanCreateResult.Value;

        sector.AssignWarehouseman(warehouseman);

        return warehouseman;
    }
}