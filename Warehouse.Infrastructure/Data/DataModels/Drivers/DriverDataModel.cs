using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data.DataModels.Shared;

namespace Warehouse.Infrastructure.Data.DataModels;

internal sealed class DriverDataModel : IDataModel<Driver>
{
    public Guid Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string VehiclePlate { get; init; }
    public ICollection<TransportDataModel> Transports { get; init; }

    private DriverDataModel(
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

    public Driver ToDomainModel()
    {
        return null;
    }
}