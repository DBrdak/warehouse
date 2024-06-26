using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Clients;

internal static class ClientErrors
{
    public static readonly Error InvalidNameError = new(
        "Nieprawidłowa nazwa kontrahenta");
    public static readonly Error InvalidNIPError = new(
        "Nieprawidłowy NIP kontrahenta");
    public static readonly Error AlreadyBookedByAnotherClient =
        new("Transport został zlecony przez innego kontrahenta");
    public static readonly Error AlreadyBookedByClient =
        new("Transport został już zlecony przez tego kontrahenta");
}