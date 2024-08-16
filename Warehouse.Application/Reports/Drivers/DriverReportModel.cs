using Warehouse.Application.Abstractions.Reports;
using Warehouse.Domain.Drivers;

namespace Warehouse.Application.Reports.Drivers;

public sealed record DriverReportModel : IReportModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string VehiclePlate { get; init; }
    public IReadOnlyCollection<DriverTransportReportModel> Transports { get; init; }

    private DriverReportModel(
        string firstName,
        string lastName,
        string vehiclePlate,
        IReadOnlyCollection<DriverTransportReportModel> transports)
    {
        FirstName = firstName;
        LastName = lastName;
        VehiclePlate = vehiclePlate;
        Transports = transports;
    }

    public static DriverReportModel FromDomainModel(Driver domainModel) =>
        new(
            domainModel.FirstName.Value,
            domainModel.LastName.Value,
            domainModel.VehiclePlate.Value,
            domainModel.Transports.Select(DriverTransportReportModel.FromDomainModel).ToList());
}