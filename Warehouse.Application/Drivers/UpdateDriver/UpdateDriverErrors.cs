using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Drivers.UpdateDriver;

internal static class UpdateDriverErrors
{
    public static readonly Error InvalidRequest = new("Wprowadzono nieprawidłowe dane dla aktualizacji kierowcy");
}