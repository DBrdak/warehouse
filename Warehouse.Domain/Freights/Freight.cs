using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Domain.Freights;

public sealed class Freight : Entity<FreightId>
{
    public FreightName Name { get; init; }
    public FreightType Type { get; init; }
    public Quantity Quantity { get; init; }
    public Unit Unit { get; init; }
    public PalletSpace PalletSpace { get; private set; }
    public Transport Import { get; init; }
    public Transport? Export { get; private set; }

    private Freight(
        FreightName name,
        FreightType type,
        Quantity quantity,
        Unit unit,
        PalletSpace palletSpace,
        Transport import,
        Transport? export,
        FreightId? id = null) : base(id)
    {
        Name = name;
        Type = type;
        Quantity = quantity;
        Unit = unit;
        PalletSpace = palletSpace;
        Import = import;
        Export = export;
    }

    internal static Result<Freight> Create(
        string name,
        string type,
        decimal quantity,
        string unit,
        PalletSpace palletSpace,
        Transport import)
    {
        var (freightNameCreateResult, freightTypeCreateResult, freightQuantityCreateResult, freightUnitCreateResult) = 
            (FreightName.Create(name), FreightType.Create(type), Quantity.Create(quantity), Unit.Create(unit));

        if (Result.Aggregate(
                freightNameCreateResult,
                freightTypeCreateResult,
                freightQuantityCreateResult,
                freightUnitCreateResult) is var result && result.IsFailure)
        {
            return result.Error;
        }

        var (freightName, freightType, freightQuantity, freightUnit) = 
            (freightNameCreateResult.Value, freightTypeCreateResult.Value, freightQuantityCreateResult.Value, freightUnitCreateResult.Value);

        var isImportValid = import.Type == TransportType.Import;

        if (!isImportValid)
        {
            return FreightErrors.InvalidImport;
        }

        return new Freight(freightName, freightType, freightQuantity, freightUnit, palletSpace, import, null);
    }

    internal Result Release(Transport transport)
    {
        var isAlreadyReleased = Export is not null;

        if (isAlreadyReleased)
        {
            return FreightErrors.AlreadyReleased;
        }

        var isTransportValid = transport.Type == TransportType.Export;

        if (!isTransportValid)
        {
            return FreightErrors.InvalidExport;
        }

        Export = transport;
        
        return Result.Success();
    }
}