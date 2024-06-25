using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Domain.Warehousemen;

public sealed class Warehouseman : Entity<WarehousemanId>
{
    public IdentificationNumber IdentificationNumber { get; init; }
    public FirstName FirstName { get; init; }
    public LastName LastName { get; init; }
    public Position? Position { get; init; }
    public Sector Sector { get; init; }
    private readonly List<Transport> _transports;
    public IReadOnlyCollection<Transport> Transports => _transports;

    private Warehouseman(
        IdentificationNumber identificationNumber,
        FirstName firstName,
        LastName lastName,
        Position? position,
        Sector sector,
        List<Transport> transports,
        WarehousemanId? id = null) : base(id)
    {
        IdentificationNumber = identificationNumber;
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Sector = sector;
        _transports = transports;
    }

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
            sector,
            []);
    }
}