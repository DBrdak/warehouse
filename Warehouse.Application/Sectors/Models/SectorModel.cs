using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Application.Sectors.Models;

public sealed record SectorModel
{
    public int Number { get; init; }
    public IReadOnlyCollection<WarehousemanModel>? Warehousemen { get; init; }
    public IReadOnlyCollection<PalletSpaceModel>? PalletSpaces { get; init; }

    private SectorModel(
        int number,
        IReadOnlyCollection<WarehousemanModel>? warehousemen,
        IReadOnlyCollection<PalletSpaceModel>? palletSpaces)
    {
        Number = number;
        Warehousemen = warehousemen;
        PalletSpaces = palletSpaces;
    }

    public static SectorModel FromDomainModel<TCaller>(Sector sector) =>
        typeof(TCaller) switch
        {
            var callerType when callerType == typeof(WarehousemanModel) => new(
                sector.Number.Value,
                null,
                sector.PalletSpaces.Select(PalletSpaceModel.FromDomainModel<SectorModel>).ToList()),
            var callerType when callerType == typeof(WarehousemanModel) => new(
                sector.Number.Value,
                sector.Warehousemen.Select(WarehousemanModel.FromDomainModel<SectorModel>).ToList(),
                null),
            _ => FromDomainModel(sector)
        };

    public static SectorModel FromDomainModel(Sector sector) =>
        new(
            sector.Number.Value,
            sector.Warehousemen.Select(WarehousemanModel.FromDomainModel<SectorModel>).ToList(),
            sector.PalletSpaces.Select(PalletSpaceModel.FromDomainModel<SectorModel>).ToList());
}