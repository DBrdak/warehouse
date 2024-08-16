using Warehouse.Application.Shared.Models;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Drivers;

namespace Warehouse.Application.Drivers.Models;

public sealed record DriverModel : BusinessModel<Driver, DriverId>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string VehiclePlate { get; init; }
    public IReadOnlyCollection<TransportModel>? Transports { get; init; }

    public DriverModel() : base(Guid.NewGuid())
    {
        FirstName = "";
        LastName = "";
        VehiclePlate = "";
        Transports = [];
    }

    private DriverModel(
        Guid id,
        string firstName,
        string lastName,
        string vehiclePlate,
        IReadOnlyCollection<TransportModel>? transports) : base(id)
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
                driver.Id.Id,
                driver.FirstName.Value,
                driver.LastName.Value,
                driver.VehiclePlate.Value,
                null),
            _ => FromDomainModel(driver)
        };

    public static DriverModel FromDomainModel(Driver driver) =>
        new(
            driver.Id.Id,
            driver.FirstName.Value,
            driver.LastName.Value,
            driver.VehiclePlate.Value,
            driver.Transports?.Select(TransportModel.FromDomainModel<DriverModel>).ToList());

    public DriverModel Copy() => new(this);
}