using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Domain.Drivers;

public sealed class Driver : Entity<DriverId>
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public VehiclePlate VehiclePlate { get; private set; }
    private readonly List<Transport> _transports;
    public IReadOnlyCollection<Transport> Transports => _transports;

    private Driver(FirstName firstName, LastName lastName, VehiclePlate vehiclePlate, List<Transport> transports, DriverId? id = null) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        VehiclePlate = vehiclePlate;
        _transports = transports;
    }

    public static Result<Driver> Create(string firstName, string lastName, string plate)
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

    public Result EditFirstName(string firstName)
    {
        var firstNameCreateResult = FirstName.Create(firstName);

        if (firstNameCreateResult.IsFailure)
        {
            return firstNameCreateResult.Error;
        }

        var driverFirstName = firstNameCreateResult.Value;

        FirstName = driverFirstName;

        return Result.Success();
    }

    public Result EditLastName(string lastName)
    {
        var lastNameCreateResult = LastName.Create(lastName);

        if (lastNameCreateResult.IsFailure)
        {
            return lastNameCreateResult.Error;
        }

        var driverLastName = lastNameCreateResult.Value;

        LastName = driverLastName;
        
        return Result.Success();
    }

    public Result EditVehiclePlate(string vehiclePlate)
    {
        var vehiclePlateCreateResult = VehiclePlate.Create(vehiclePlate);

        if (vehiclePlateCreateResult.IsFailure)
        {
            return vehiclePlateCreateResult.Error;
        }

        var driverVehiclePlate = vehiclePlateCreateResult.Value;

        VehiclePlate = driverVehiclePlate;

        return Result.Success();
    }

    internal Result DeliverTransport(Transport transport)
    {
        var isTransportAlreadyDeliveredByDriver = _transports.Any(t => t.Id == transport.Id);

        if (isTransportAlreadyDeliveredByDriver)
        {
            return DriverErrors.TransportAlreadyDeliveredByDriver;
        }

        var isTransportAlreadyDeliveredByAnotherDriver = transport.Driver.Id != transport.Id;

        if (isTransportAlreadyDeliveredByAnotherDriver)
        {
            return DriverErrors.TransportAlreadyDeliveredByAnotherDriver;
        }

        return Result.Success();
    }
}