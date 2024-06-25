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
    public Sector Sector { get; init; }
    private readonly List<Freight> _freights;
    public IReadOnlyCollection<Freight> Freights => _freights;

    private PalletSpace(
        PalletSpaceNumber number,
        Shelf shelf,
        Rack rack,
        Sector sector,
        List<Freight> freights,
        PalletSpaceId? id = null) : base(id)
    {
        Number = number;
        Shelf = shelf;
        Rack = rack;
        Sector = sector;
        _freights = freights;
    }

    internal Result<PalletSpace> Create(
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

        return new PalletSpace(palletSpaceNumber, shelfNumber, rackNumber, sector, []);
    }
}