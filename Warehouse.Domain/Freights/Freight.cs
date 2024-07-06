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
    public PalletSpaceId PalletSpaceId { get; init; }
    public PalletSpace PalletSpace { get; init; }
    public TransportId ImportId { get; init; }
    public Transport Import { get; init; }
    public TransportId? ExportId { get; private set; }
    public Transport? Export { get; private set; }

    private Freight(
        FreightName name,
        FreightType type,
        Quantity quantity,
        Unit unit,
        PalletSpaceId palletSpaceId,
        PalletSpace palletSpace,
        TransportId importId,
        Transport import,
        TransportId? exportId,
        Transport? export,
        FreightId? id = null) : base(id)
    {
        Name = name;
        Type = type;
        Quantity = quantity;
        Unit = unit;
        PalletSpaceId = palletSpaceId;
        PalletSpace = palletSpace;
        ImportId = importId;
        Import = import;
        ExportId = exportId;
        Export = export;
    }

    private Freight() : base()
    { }

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

        return new Freight(freightName, freightType, freightQuantity, freightUnit, palletSpace.Id, palletSpace, import.Id, import, null, null);
    }

    private void SetExportId(Transport export) => (ExportId, Export) = (export.Id, export);

    internal Result Release(Transport export)
    {
        var isAlreadyReleased = Export is not null;

        if (isAlreadyReleased)
        {
            return FreightErrors.AlreadyReleased;
        }

        var isTransportValid = export.Type == TransportType.Export;

        if (!isTransportValid)
        {
            return FreightErrors.InvalidExport;
        }

        SetExportId(export);
        
        return Result.Success();
    }
}