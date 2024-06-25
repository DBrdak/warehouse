using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Domain.Drivers;

public sealed class Driver : Entity<DriverId>
{
    public FirstName FirstName { get; init; }
    public LastName LastName { get; init; }
    public VehiclePlate VehiclePlate { get; init; }
    private readonly List<Transport> _transports;
    public IReadOnlyCollection<Transport> Transports => _transports;

    private Driver(FirstName firstName, LastName lastName, VehiclePlate vehiclePlate, List<Transport> transports, DriverId? id = null) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        VehiclePlate = vehiclePlate;
        _transports = transports;
    }

    internal static Result<Driver> Create(string firstName, string lastName, string plate)
    {
        var driverFirstNameCreateReult = FirstName.Create(firstName);

        if (driverFirstNameCreateReult.IsFailure)
        {
            return driverFirstNameCreateReult.Error;
        }

        var driverLastNameCreateReult = LastName.Create(lastName);

        if (driverLastNameCreateReult.IsFailure)
        {
            return driverLastNameCreateReult.Error;
        }

        var vehiclePlateCreateReult = VehiclePlate.Create(plate);

        if (vehiclePlateCreateReult.IsFailure)
        {
            return vehiclePlateCreateReult.Error;
        }

        var driverFirstName = driverFirstNameCreateReult.Value;
        var driverLastName = driverLastNameCreateReult.Value;
        var vehiclePlate = vehiclePlateCreateReult.Value;

        return new Driver(driverFirstName, driverLastName, vehiclePlate, []);
    }
}