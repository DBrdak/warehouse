using Warehouse.Domain.Clients;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Freights;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Domain.Transports;

public sealed class Transport : Entity<TransportId>
{
    public TransportNumber Number { get; init; }
    public TransportType Type { get; init; }
    public DateTime HandledAt { get; init; }
    public WarehousemanId WarehousemanId { get; init; }
    public Warehouseman Warehouseman { get; init; }
    public DriverId DriverId { get; init; }
    public Driver Driver { get; init; }
    public ClientId ClientId { get; init; }
    public Client Client { get; init; }

    private readonly List<Freight> _deliveredFreights;
    public IReadOnlyCollection<Freight> DeliveredFreights => _deliveredFreights;

    private readonly List<Freight> _receivedFreights;
    public IReadOnlyCollection<Freight> ReceivedFreights => _receivedFreights;
    public IReadOnlyCollection<Freight> Freights => Type == TransportType.Export ?
        _receivedFreights :
        _deliveredFreights;

    private Transport(
        TransportNumber number,
        TransportType type,
        DateTime handledAt,
        WarehousemanId warehousemanId,
        Warehouseman warehouseman,
        DriverId driverId,
        Driver driver,
        ClientId clientId,
        Client client,
        List<Freight> deliveredFreights,
        List<Freight> receivedFreights,
        TransportId? id = null) : base(id)
    {
        Number = number;
        Type = type;
        HandledAt = handledAt;
        WarehousemanId = warehousemanId;
        Warehouseman = warehouseman;
        DriverId = driverId;
        Driver = driver;
        ClientId = clientId;
        Client = client;
        _deliveredFreights = deliveredFreights;
        _receivedFreights = receivedFreights;
    }


    private Transport() : base()
    { }

    internal static Result<Transport> Create(
        int number,
        string type,
        Warehouseman warehouseman,
        Driver driver,
        Client client,
        DateTime? handledAt = null)
    {
        var (transportNumberCreateResult, transportTypeCreateResult) =
            (TransportNumber.Create(number), TransportType.Create(type));

        if (Result.Aggregate(
                transportTypeCreateResult,
                transportTypeCreateResult) is var result &&
            result.IsFailure)
        {
            return result.Error;
        }

        var (transportNumber, transportType) =
            (transportNumberCreateResult.Value, transportTypeCreateResult.Value);

        return new Transport(
            transportNumber,
            transportType,
            handledAt ?? DateTime.UtcNow,
            warehouseman.Id,
            warehouseman,
            driver.Id,
            driver,
            client.Id,
            client,
            [],
            []);
    }

    internal Result AddFreight(Freight freight)
    {
        var isAlreadyContainFreight = Type == TransportType.Import ?
            _deliveredFreights.Any(f => f.Id == Id) :
            _receivedFreights.Any(f => f.Id == Id);

        if (isAlreadyContainFreight)
        {
            return TransportErrors.AlreadyContainFreight;
        }

        switch(Type)
        {
            case var type when type == TransportType.Import:
                _deliveredFreights.Add(freight);
                break;
            case var type when type == TransportType.Export:
                _receivedFreights.Add(freight);
                break;
        };

        return Result.Success();
    }
}