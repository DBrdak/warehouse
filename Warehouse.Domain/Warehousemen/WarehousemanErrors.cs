using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Warehousemen;

internal static class WarehousemanErrors
{
    public static readonly Error InvalidWarehousemanPosition =
        new("Nieprawidłowe stanowisko magazyniera");
    public static readonly Error InvalidWarehousemanIdentificationNumber =
        new("Nieprawidłowy numer identyfikacyjny magazyniera");
}