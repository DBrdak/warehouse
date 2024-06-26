using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces;

internal static class PalletSpaceErrors
{
    public static readonly Error InvalidNumber = new("Nieprawidłowy numer miejsca paletowego");
    public static readonly Error InvalidRackNumber = new("Nieprawidłowy numer regału");
    public static readonly Error InvalidShelfNumber = new("Nieprawidłowy numer półki");
    public static readonly Error FreightIsExported = new ("Towar został już wydany");
    public static readonly Error FreightAlreadyAtOtherPalletSpace =
        new("Towar znajduje się już na innym miejscu paletowym");
    public static readonly Error FreightAlreadyAtPalletSpace =
        new("Towar znajduje się już na tym miejscu paletowym");
    public static readonly Error FreightNotInExport =
        new("Towar bez odbioru - nie można zwolnić miejsca paletowego");
    public static readonly Error FreightNotAtThisPalletSpace =
        new("Towar nie jest przypisany do tego miejsca paletowego");
}