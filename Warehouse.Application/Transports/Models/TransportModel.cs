using Warehouse.Application.Clients.Models;
using Warehouse.Application.Drivers.Models;
using Warehouse.Application.Freights.Models;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Transports;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Application.Transports.Models;

public sealed record TransportModel
{
    public int Number { get; init; }
    public string Type { get; init; }
    public DateTime HandledAt { get; init; }
    public WarehousemanModel? Warehouseman { get; init; }
    public DriverModel? Driver { get; init; }
    public ClientModel? Client { get; init; }
    public IReadOnlyCollection<FreightModel>? Freights { get; init; }

    private TransportModel(
        int number,
        string type,
        DateTime handledAt,
        WarehousemanModel? warehouseman,
        DriverModel? driver,
        ClientModel? client,
        IReadOnlyCollection<FreightModel>? freights)
    {
        Number = number;
        Type = type;
        HandledAt = handledAt;
        Warehouseman = warehouseman;
        Driver = driver;
        Client = client;
        Freights = freights;
    }

    internal static TransportModel FromDomainModel<TCaller>(Transport transport) =>
        typeof(TCaller) switch
        { 
            var callerType when callerType == typeof(WarehousemanModel) => new(
                transport.Number.Value,
                transport.Type.Value,
                transport.HandledAt,
                null,
                DriverModel.FromDomainModel<TransportModel>(transport.Driver),
                ClientModel.FromDomainModel<TransportModel>(transport.Client),
                transport.Freights.Select(f => FreightModel.FromDomainModel<TransportModel>(f, transport.Type.IsImport)).ToList()),
            var callerType when callerType == typeof(DriverModel) => new(
                transport.Number.Value,
                transport.Type.Value,
                transport.HandledAt,
                WarehousemanModel.FromDomainModel<TransportModel>(transport.Warehouseman),
                null,
                ClientModel.FromDomainModel<TransportModel>(transport.Client),
                transport.Freights.Select(f => FreightModel.FromDomainModel<TransportModel>(f, transport.Type.IsImport)).ToList()),
            var callerType when callerType == typeof(ClientModel) => new(
                transport.Number.Value,
                transport.Type.Value,
                transport.HandledAt,
                WarehousemanModel.FromDomainModel<TransportModel>(transport.Warehouseman),
                DriverModel.FromDomainModel<TransportModel>(transport.Driver),
                null,
                transport.Freights.Select(f => FreightModel.FromDomainModel<TransportModel>(f, transport.Type.IsImport)).ToList()),
            var callerType when callerType == typeof(ClientModel) => new(
                transport.Number.Value,
                transport.Type.Value,
                transport.HandledAt,
                WarehousemanModel.FromDomainModel<TransportModel>(transport.Warehouseman),
                DriverModel.FromDomainModel<TransportModel>(transport.Driver),
                ClientModel.FromDomainModel<TransportModel>(transport.Client),
                null),
            _ => FromDomainModel(transport)
        };

    internal static TransportModel FromDomainModel(Transport transport) =>
        new(
            transport.Number.Value,
            transport.Type.Value,
            transport.HandledAt,
            WarehousemanModel.FromDomainModel<TransportModel>(transport.Warehouseman),
            DriverModel.FromDomainModel<TransportModel>(transport.Driver),
            ClientModel.FromDomainModel<TransportModel>(transport.Client),
            transport.Freights.Select(f => FreightModel.FromDomainModel<TransportModel>(f, transport.Type.IsImport)).ToList());
}