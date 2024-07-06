using Warehouse.Domain.Freights;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces;

public sealed class PalletSpace : Entity<PalletSpaceId>
{
    public PalletSpaceNumber Number { get; init; }
    public Shelf Shelf { get; init; }
    public Rack Rack { get; init; }
    public SectorId SectorId { get; init; }
    public Sector Sector { get; init; }
    private readonly List<Freight> _freights;
    public IReadOnlyCollection<Freight> Freights => _freights;

    private PalletSpace(
        PalletSpaceNumber number,
        Shelf shelf,
        Rack rack,
        SectorId sectorId,
        Sector sector,
        List<Freight> freights,
        PalletSpaceId? id = null) : base(id)
    {
        Number = number;
        Shelf = shelf;
        Rack = rack;
        SectorId = sectorId;
        Sector = sector;
        _freights = freights;
    }


    private PalletSpace() : base()
    { }

    internal static Result<PalletSpace> Create(
        int number,
        int shelf,
        int rack,
        Sector sector)
    {
        var (palletSpaceNumberCreateResult, shelfNumberCreateResult, rackNumberCreateResult) =
            (PalletSpaceNumber.Create(number), Shelf.Create(shelf), Rack.Create(rack));

        if (Result.Aggregate(
                palletSpaceNumberCreateResult,
                shelfNumberCreateResult,
                rackNumberCreateResult) is var result &&
            result.IsFailure)
        {
            return result.Error;
        }

        var (palletSpaceNumber, shelfNumber, rackNumber) = 
            (palletSpaceNumberCreateResult.Value, shelfNumberCreateResult.Value, rackNumberCreateResult.Value);

        return new PalletSpace(palletSpaceNumber, shelfNumber, rackNumber, sector.Id, sector, []);
    }

    internal Result PlaceFreight(Freight freight)
    {
        var freightAlreadyAtPalletSpace = _freights.Any(f => f.Id == freight.Id);

        if (freightAlreadyAtPalletSpace)
        {
            return PalletSpaceErrors.FreightAlreadyAtPalletSpace;
        }

        var freightAlreadyAtOtherPalletSpace = freight.PalletSpace.Id != Id;

        if (freightAlreadyAtOtherPalletSpace)
        {
            return PalletSpaceErrors.FreightAlreadyAtOtherPalletSpace;
        }

        var freightIsExported = freight.Export is not null;

        if (freightIsExported)
        {
            return PalletSpaceErrors.FreightIsExported;
        }

        _freights.Add(freight);

        return Result.Success();
    }

    internal Result TakeFreight(Freight freight)
    {
        var freightNotAtThisPalletSpace = freight.PalletSpace.Id != Id;

        if (freightNotAtThisPalletSpace)
        {
            return PalletSpaceErrors.FreightNotAtThisPalletSpace;
        }

        var freightNotInExport = freight.Export is null;

        if (freightNotInExport)
        {
            return PalletSpaceErrors.FreightNotInExport;
        }

        _freights.Remove(freight);

        return Result.Success();
    }
}