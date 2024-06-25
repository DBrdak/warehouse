using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Transports;

internal static class TransportErrors
{
    public static readonly Error InvalidTransportNumber = new("Nieprawidłowy numer transportu");
    public static readonly Error InvalidTransportType = new("Niewłaściwy rodzaj transportu");
    public static readonly Error AlreadyContainFreight = new ("Transport zawiera ten towar");
}