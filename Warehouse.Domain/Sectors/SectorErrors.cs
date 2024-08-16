using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Sectors;

internal static class SectorErrors
{
    public static readonly Error InvalidSectorNumber = new("Nieprawidłowy numer sektora");
    public static readonly Error WarehousemanAlreadyAssigned = new ("Magazynier już obsługuje ten sektor");
    public static readonly Error PalletSpaceAlreadyExists = new ("Miejsce paletowe już istnieje w sektorze");
}