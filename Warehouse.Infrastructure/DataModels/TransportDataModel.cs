using Warehouse.Domain.Clients;

namespace Warehouse.Infrastructure.DataModels;

internal class TransportDataModel
{
    public Guid Id { get; init; }
    public Guid WarehousemanId { get; init; }
    public Guid DriverId { get; init; }
    public Guid ClientId { get; init; }
    public int Number { get; init; }
    public string Type { get; init; }
    //TODO Convert to UTC when saving, convert to local then querying
    public DateTime HandledAt {get; init; }
    public WarehousemanDataModel Warehouseman { get; init; }
    public DriverDataModel Driver { get; init; }
    public ClientDataModel Client { get; init; }
    public ICollection<FreightDataModel> Freights { get; init; }


    public TransportDataModel(
        Guid id,
        Guid warehousemanId,
        Guid driverId,
        Guid clientId,
        int number,
        string type,
        DateTime handledAt,
        WarehousemanDataModel warehouseman,
        DriverDataModel driver,
        ClientDataModel client,
        ICollection<FreightDataModel> freights)
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
}