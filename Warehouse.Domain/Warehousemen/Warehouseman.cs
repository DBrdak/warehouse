using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Domain.Warehousemen;

public sealed class Warehouseman : Entity<WarehousemanId>
{
    public IdentificationNumber IdentificationNumber { get; init; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Position? Position { get; private set; }
    public bool IsFired { get; private set; }
    public SectorId SectorId { get; private set; }
    public Sector Sector { get; private set; }
    private readonly List<Transport> _transports;
    public IReadOnlyCollection<Transport> Transports => _transports;

    private Warehouseman(
        IdentificationNumber identificationNumber,
        FirstName firstName,
        LastName lastName,
        Position? position,
        SectorId sectorId,
        Sector sector,
        List<Transport> transports,
        bool isFired,
        WarehousemanId? id = null) : base(id)
    {
        IdentificationNumber = identificationNumber;
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        SectorId = sectorId;
        Sector = sector;
        _transports = transports;
        IsFired = isFired;
    }


    private Warehouseman() : base()
    { }

    internal static Result<Warehouseman> Create(
        int identificationNumber,
        string firstName,
        string lastName,
        string? position,
        Sector sector)
    {
        var (warehousemanIdentificationNumberCreateResult, warehousemanFirstNameCreateResult,
            warehousemanLastNameCreateResult, warehousemanPositionCreateResult) = (
            IdentificationNumber.Create(identificationNumber),
            FirstName.Create(firstName),
            LastName.Create(lastName),
            position is null ? null : Position.Create(position));

        if (Result.Aggregate(
                warehousemanIdentificationNumberCreateResult,
                warehousemanFirstNameCreateResult,
                warehousemanLastNameCreateResult,
                warehousemanPositionCreateResult ?? Result.Success()) is var result &&
            result.IsFailure)
        {
            return result.Error;
        }

        var (warehousemanIdentificationNumber, warehousemanFirstName, warehousemanLastName, warehousemanPosition) = 
            (warehousemanIdentificationNumberCreateResult.Value,
            warehousemanFirstNameCreateResult.Value, warehousemanLastNameCreateResult.Value,
            warehousemanPositionCreateResult?.Value);

        return new Warehouseman(
            warehousemanIdentificationNumber,
            warehousemanFirstName,
            warehousemanLastName,
            warehousemanPosition,
            sector.Id,
            sector,
            [],
            false);
    }

    public Result EditFirstName(string firstName)
    {
        var firstNameCreateResult = FirstName.Create(firstName);

        if (firstNameCreateResult.IsFailure)
        {
            return firstNameCreateResult.Error;
        }

        var warehousemanFirstName = firstNameCreateResult.Value;

        FirstName = warehousemanFirstName;

        return Result.Success();
    }

    public Result EditLastName(string lastName)
    {
        var lastNameCreateResult = LastName.Create(lastName);

        if (lastNameCreateResult.IsFailure)
        {
            return lastNameCreateResult.Error;
        }

        var warehousemanLastName = lastNameCreateResult.Value;

        LastName = warehousemanLastName;

        return Result.Success();
    }

    public Result EditPosition(string position)
    {
        var positionCreateResult = Position.Create(position);

        if (positionCreateResult.IsFailure)
        {
            return positionCreateResult.Error;
        }

        var warehousemanPosition = positionCreateResult.Value;

        Position = warehousemanPosition;

        return Result.Success();
    }

    public void MoveToSector(Sector sector)
    {
        SectorId = sector.Id;
        Sector = sector;
    }

    internal Result HandleTransport(Transport transport)
    {
        var isAlreadyHandledByWarehouseman = _transports.Any(t => t.Id == transport.Id);

        if (isAlreadyHandledByWarehouseman)
        {
            return WarehousemanErrors.AlreadyHandledByWarehouseman;
        }

        var isAlreadyHandledByOtherWarehouseman = transport.Warehouseman.Id != Id;

        if (isAlreadyHandledByOtherWarehouseman)
        {
            return WarehousemanErrors.AlreadyHandledByOtherWarehouseman;
        }

        _transports.Add(transport);

        return Result.Success();
    }

    public void Fire() => IsFired = true;
}