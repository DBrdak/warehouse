using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Drivers;

namespace Warehouse.Application.Drivers.Models;

public sealed record DriverModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string VehiclePlate { get; init; }
    public IReadOnlyCollection<TransportModel>? Transports { get; init; }

    private DriverModel(
        string firstName,
        string lastName,
        string vehiclePlate,
        IReadOnlyCollection<TransportModel>? transports)
    {
        FirstName = firstName;
        LastName = lastName;
        VehiclePlate = vehiclePlate;
        Transports = transports;
    }

    public static DriverModel FromDomainModel<TCaller>(Driver driver) =>
        typeof(TCaller) switch
        {
            var callerType when callerType == typeof(TransportModel) => new(
                driver.FirstName.Value,
                driver.LastName.Value,
                driver.VehiclePlate.Value,
                null),
            _ => FromDomainModel(driver)
        };

    public static DriverModel FromDomainModel(Driver driver)
    {
        return null;
    }
}