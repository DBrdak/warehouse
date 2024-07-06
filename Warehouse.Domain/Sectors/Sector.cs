using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Domain.Sectors;

public sealed class Sector : Entity<SectorId>
{
    public SectorNumber Number { get; init; }
    private readonly List<Warehouseman> _warehousemen;
    public IReadOnlyCollection<Warehouseman> Warehousemen => _warehousemen;
    private readonly List<PalletSpace> _palletSpaces;
    public IReadOnlyCollection<PalletSpace> PalletSpaces => _palletSpaces;

    private Sector(
        SectorNumber number,
        List<Warehouseman> warehousemen,
        List<PalletSpace> palletSpaces,
        SectorId? id = null) : base(id)
    {
        Number = number;
        _warehousemen = warehousemen;
        _palletSpaces = palletSpaces;
    }


    private Sector() : base()
    { }

    public static Result<Sector> Create(int number)
    {
        var sectorNumberCreateResult = SectorNumber.Create(number);

        if (sectorNumberCreateResult.IsFailure)
        {
            return sectorNumberCreateResult.Error;
        }

        var sectorNumber = sectorNumberCreateResult.Value;

        return new Sector(sectorNumber, [], []);
    }

    internal Result AssignWarehouseman(Warehouseman warehouseman)
    {
        var isAlreadyAssigned = _warehousemen.Any(w => w.Id == warehouseman.Id);

        if (isAlreadyAssigned)
        {
            return SectorErrors.WarehousemanAlreadyAssigned;
        }

        _warehousemen.Add(warehouseman);

        return Result.Success();
    }

    internal Result AssignPalletSpace(PalletSpace palletSpace)
    {
        var isPalletSpaceAlreadyInSector = _palletSpaces.Any(
            ps => ps.Number == palletSpace.Number &&
                  ps.Shelf == palletSpace.Shelf &&
                  ps.Rack == palletSpace.Rack);

        if (isPalletSpaceAlreadyInSector)
        {
            return SectorErrors.PalletSpaceAlreadyExists;
        }

        _palletSpaces.Add(palletSpace);

        return Result.Success();
    }
}