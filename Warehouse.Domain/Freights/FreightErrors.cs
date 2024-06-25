using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Freights;

internal static class FreightErrors
{
    public static readonly Error InvalidFreightName = new("Nieprawidłowa nazwa towaru");
    public static readonly Error InvalidFreightType = new("Nieprawidłowy rodzaj towaru");
    public static readonly Error InvalidUnit = new("Nieprawidłowa jednostka");
    public static readonly Error InvalidQuantity = new("Nieprawidłowa ilość");
    public static readonly Error InvalidImport = new("Nieprawidłowy typ importu");
    public static readonly Error InvalidExport = new("Nieprawidłowy typ eksportu");
}