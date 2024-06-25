using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Drivers;

internal static class DriverErrors
{
    public static readonly Error InvalidVehiclePlateNumber =
        new("Nieprawidłowe numery rejestracyjne pojazdu");
}