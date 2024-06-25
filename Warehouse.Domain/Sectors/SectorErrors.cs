using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Sectors;

internal static class SectorErrors
{
    public static readonly Error InvalidSectorNumber = new("Nieprawidłowy numer sektora");
}