using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Abstractions.Reports;
using Warehouse.Application.Reports.Drivers;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Reports.Clients;

public sealed record ClientTransportReportModel : IReportModel
{
    public int Number { get; init; }
    public string Type { get; init; }
    public DateTime HandledAt { get; init; }
    public int WarehousemanIdentificationNumber { get; init; }
    public string DriverVehiclePlate { get; init; }

    private ClientTransportReportModel(
        int number,
        string type,
        DateTime handledAt,
        int warehousemanIdentificationNumber,
        string driverVehiclePlate)
    {
        Number = number;
        Type = type;
        HandledAt = handledAt;
        WarehousemanIdentificationNumber = warehousemanIdentificationNumber;
        DriverVehiclePlate = driverVehiclePlate;
    }

    public static ClientTransportReportModel FromDomainModel(Transport domainModel) =>
        new(
            domainModel.Number.Value,
            domainModel.Type.Value,
            domainModel.HandledAt,
            domainModel.Warehouseman.IdentificationNumber.Value,
            domainModel.Driver.VehiclePlate.Value);

}