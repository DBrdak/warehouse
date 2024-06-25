using System.Text.RegularExpressions;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Drivers;

public sealed record VehiclePlate
{
    private static readonly Regex pattern = new(@"^[A-Z]{1,3}[A-Z0-9]{4,5}$");
    public string Value { get; init; }

    private VehiclePlate(string value) => Value = value;

    internal static Result<VehiclePlate> Create(string value)
    {
        var isValid = pattern.IsMatch(value);

        if (!isValid)
        {
            return DriverErrors.InvalidVehiclePlateNumber;
        }

        return new VehiclePlate(value);
    }
}