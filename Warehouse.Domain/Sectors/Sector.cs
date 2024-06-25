using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Domain.Sectors;

public sealed class Sector : Entity<SectorId>
{
    public SectorNumber Number { get; init; }
    private readonly List<Warehouseman> _warehousemen;
    public IReadOnlyCollection<Warehouseman> Warehousemen => _warehousemen;

    private Sector(SectorNumber number, List<Warehouseman> warehousemen, SectorId? id = null) : base(id)
    {
        Number = number;
        _warehousemen = warehousemen;
    }

    internal static Result<Sector> Create(int number)
    {
        var sectorNumberCreateResult = SectorNumber.Create(number);

        if (sectorNumberCreateResult.IsFailure)
        {
            return sectorNumberCreateResult.Error;
        }

        var sectorNumber = sectorNumberCreateResult.Value;

        return new Sector(sectorNumber, []);
    }
}