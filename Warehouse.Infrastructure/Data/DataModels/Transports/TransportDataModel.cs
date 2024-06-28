using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;
using Warehouse.Infrastructure.Data.DataModels.Shared;

namespace Warehouse.Infrastructure.Data.DataModels;

internal class TransportDataModel : IDataModel<Transport>
{
    public Guid Id { get; init; }
    public Guid WarehousemanId { get; init; }
    public Guid DriverId { get; init; }
    public Guid ClientId { get; init; }
    public int Number { get; init; }
    public string Type { get; init; }
    //TODO Convert to UTC when saving, convert to local then querying
    public DateTime HandledAt {get; init; }
    public WarehousemanDataModel? Warehouseman { get; init; }
    public DriverDataModel? Driver { get; init; }
    public ClientDataModel? Client { get; init; }
    public ICollection<FreightDataModel>? Freights { get; init; }


    private TransportDataModel(
        Guid id,
        Guid warehousemanId,
        Guid driverId,
        Guid clientId,
        int number,
        string type,
        DateTime handledAt,
        WarehousemanDataModel? warehouseman,
        DriverDataModel? driver,
        ClientDataModel? client,
        ICollection<FreightDataModel>? freights)
    {
        Id = id;
        WarehousemanId = warehousemanId;
        DriverId = driverId;
        ClientId = clientId;
        Number = number;
        Type = type;
        HandledAt = handledAt;
        Warehouseman = warehouseman;
        Driver = driver;
        Client = client;
        Freights = freights;
    }

    public Transport ToDomainModel()
    {
        return null;
    }

    //TODO resolve the issue of loop references
    public static TransportDataModel FromDomainModel(Transport domainModel) =>
        new(
            domainModel.Id.Id,
            domainModel.Warehouseman.Id.Id,
            domainModel.Driver.Id.Id,
            domainModel.Client.Id.Id,
            domainModel.Number.Value,
            domainModel.Type.Value,
            domainModel.HandledAt,
            WarehousemanDataModel.FromDomainModel(domainModel.Warehouseman),
            DriverDataModel.FromDomainModel(domainModel.Driver),
            ClientDataModel.FromDomainModel(domainModel.Client),
            domainModel.Freights.Select(FreightDataModel.FromDomainModel));
}