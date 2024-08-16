using Warehouse.Application.Clients.Models;
using Warehouse.Application.Drivers.Models;
using Warehouse.Application.Freights.Models;
using Warehouse.Application.Shared.Models;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Transports.Models;

public sealed record TransportModel : BusinessModel<Transport, TransportId>
{
    public int Number { get; init; }
    public string Type { get; init; }
    public DateTime HandledAt { get; init; }
    public WarehousemanModel? Warehouseman { get; init; }
    public DriverModel? Driver { get; init; }
    public ClientModel? Client { get; init; }
    public IReadOnlyCollection<FreightModel>? Freights { get; init; }

    private TransportModel(
        Guid id,
        int number,
        string type,
        DateTime handledAt,
        WarehousemanModel? warehouseman,
        DriverModel? driver,
        ClientModel? client,
        IReadOnlyCollection<FreightModel>? freights) : base(id)
    {
        Number = number;
        Type = type;
        HandledAt = handledAt.ToLocalTime();
        Warehouseman = warehouseman;
        Driver = driver;
        Client = client;
        Freights = freights;
    }

    internal static TransportModel FromDomainModel<TCaller>(Transport transport) =>
        typeof(TCaller) switch
        { 
            var callerType when callerType == typeof(WarehousemanModel) => new(
                transport.Id.Id,
                transport.Number.Value,
                transport.Type.Value,
                transport.HandledAt,
                null,
                transport.Driver is null ? null : DriverModel.FromDomainModel<TransportModel>(transport.Driver),
                transport.Client is null ? null : ClientModel.FromDomainModel<TransportModel>(transport.Client),
                transport.Freights?.Select(f => FreightModel.FromDomainModel<TransportModel>(f, transport.Type.IsImport)).ToList()),
            var callerType when callerType == typeof(DriverModel) => new(
                transport.Id.Id,
                transport.Number.Value,
                transport.Type.Value,
                transport.HandledAt,
                WarehousemanModel.FromDomainModel<TransportModel>(transport.Warehouseman),
                null,
                ClientModel.FromDomainModel<TransportModel>(transport.Client),
                transport.Freights.Select(f => FreightModel.FromDomainModel<TransportModel>(f, transport.Type.IsImport)).ToList()),
            var callerType when callerType == typeof(ClientModel) => new(
                transport.Id.Id,
                transport.Number.Value,
                transport.Type.Value,
                transport.HandledAt,
                WarehousemanModel.FromDomainModel<TransportModel>(transport.Warehouseman),
                DriverModel.FromDomainModel<TransportModel>(transport.Driver),
                null,
                transport.Freights.Select(f => FreightModel.FromDomainModel<TransportModel>(f, transport.Type.IsImport)).ToList()),
            var callerType when callerType == typeof(FreightModel) => new(
                transport.Id.Id,
                transport.Number.Value,
                transport.Type.Value,
                transport.HandledAt,
                transport.Warehouseman is null ? null : WarehousemanModel.FromDomainModel<TransportModel>(transport.Warehouseman),
                transport.Driver is null ? null : DriverModel.FromDomainModel<TransportModel>(transport.Driver),
                transport.Client is null ? null : ClientModel.FromDomainModel<TransportModel>(transport.Client),
                null),
            _ => FromDomainModel(transport)
        };

    internal static TransportModel FromDomainModel(Transport transport) =>
        new(
            transport.Id.Id,
            transport.Number.Value,
            transport.Type.Value,
            transport.HandledAt,
            WarehousemanModel.FromDomainModel<TransportModel>(transport.Warehouseman),
            DriverModel.FromDomainModel<TransportModel>(transport.Driver),
            ClientModel.FromDomainModel<TransportModel>(transport.Client),
            transport.Freights.Select(f => FreightModel.FromDomainModel<TransportModel>(f, transport.Type.IsImport)).ToList());
}