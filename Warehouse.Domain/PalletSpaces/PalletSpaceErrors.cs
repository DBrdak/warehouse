using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces;

internal static class PalletSpaceErrors
{
    public static readonly Error InvalidNumber = new("Nieprawidłowy numer miejsca paletowego");
    public static readonly Error InvalidRackNumber = new("Nieprawidłowy numer regału");
    public static readonly Error InvalidShelfNumber = new("Nieprawidłowy numer półki");
}