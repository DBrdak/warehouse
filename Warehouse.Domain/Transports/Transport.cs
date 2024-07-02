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

    private readonly List<Freight> _freights;
    public IReadOnlyCollection<Freight> Freights => _freights;

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
        List<Freight> freights,
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
        _freights = freights;
    }

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
            []);
    }

    internal Result AddFreight(Freight freight)
    {
        var isAlreadyContainFreight = _freights.Any(f => f.Id == Id);

        if (isAlreadyContainFreight)
        {
            return TransportErrors.AlreadyContainFreight;
        }
        
        _freights.Add(freight);

        return Result.Success();
    }
}