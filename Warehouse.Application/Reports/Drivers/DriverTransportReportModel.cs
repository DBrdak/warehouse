using Warehouse.Application.Abstractions.Reports;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Reports.Drivers;

public sealed record DriverTransportReportModel : IReportModel
{
    public int Number { get; init; }
    public string Type { get; init; }
    public DateTime HandledAt { get; init; }
    public int WarehousemanIdentificationNumber { get; init; }
    public string ClientNip { get; init; }

    private DriverTransportReportModel(
        int number,
        string type,
        DateTime handledAt,
        int warehousemanIdentificationNumber,
        string clientNip)
    {
        Number = number;
        Type = type;
        HandledAt = handledAt;
        WarehousemanIdentificationNumber = warehousemanIdentificationNumber;
        ClientNip = clientNip;
    }

    public static DriverTransportReportModel FromDomainModel(Transport domainModel) =>
        new(
            domainModel.Number.Value,
            domainModel.Type.Value,
            domainModel.HandledAt,
            domainModel.Warehouseman.IdentificationNumber.Value,
            domainModel.Client.Nip.Value);

}