using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Drivers;

internal static class DriverErrors
{
    public static readonly Error InvalidVehiclePlateNumber =
        new("Nieprawidłowe numery rejestracyjne pojazdu");
    public static readonly Error TransportAlreadyDeliveredByAnotherDriver =
        new("Transport został dostarczony przez innego kierowcę");
    public static readonly Error TransportAlreadyDeliveredByDriver =
        new("Transport został już dostarczony przez tego kierowcę");
}