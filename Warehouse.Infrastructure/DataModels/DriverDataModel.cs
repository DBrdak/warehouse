namespace Warehouse.Infrastructure.DataModels;

internal sealed class DriverDataModel
{
    public Guid Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string VehiclePlate { get; init; }
    public ICollection<TransportDataModel> Transports { get; init; }

    public DriverDataModel(
        Guid id,
        string firstName,
        string lastName,
        string vehiclePlate,
        ICollection<TransportDataModel> transports)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        VehiclePlate = vehiclePlate;
        Transports = transports;
    }
}