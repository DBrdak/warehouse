using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Application.Shared.Models;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Sectors;

namespace Warehouse.Application.Sectors.Models;

public sealed record SectorModel : BusinessModel<Sector, SectorId>
{
    public int Number { get; init; }
    public IReadOnlyCollection<WarehousemanModel>? Warehousemen { get; init; }
    public IReadOnlyCollection<PalletSpaceModel>? PalletSpaces { get; init; }

    private SectorModel(
        Guid id,
        int number,
        IReadOnlyCollection<WarehousemanModel>? warehousemen,
        IReadOnlyCollection<PalletSpaceModel>? palletSpaces) : base(id)
    {
        Number = number;
        Warehousemen = warehousemen;
        PalletSpaces = palletSpaces;
    }

    public static SectorModel FromDomainModel<TCaller>(Sector sector) =>
        typeof(TCaller) switch
        {
            var callerType when callerType == typeof(WarehousemanModel) => new(
                sector.Id.Id,
                sector.Number.Value,
                null,
                sector.PalletSpaces.Select(PalletSpaceModel.FromDomainModel<SectorModel>).ToList()),
            var callerType when callerType == typeof(WarehousemanModel) => new(
                sector.Id.Id,
                sector.Number.Value,
                sector.Warehousemen.Select(WarehousemanModel.FromDomainModel<SectorModel>).ToList(),
                null),
            _ => FromDomainModel(sector)
        };

    public static SectorModel FromDomainModel(Sector sector) =>
        new(
            sector.Id.Id,
            sector.Number.Value,
            sector.Warehousemen.Select(WarehousemanModel.FromDomainModel<SectorModel>).ToList(),
            sector.PalletSpaces.Select(PalletSpaceModel.FromDomainModel<SectorModel>).ToList());
}