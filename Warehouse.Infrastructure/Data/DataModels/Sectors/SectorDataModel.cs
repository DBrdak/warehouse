using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data.DataModels.Shared;

namespace Warehouse.Infrastructure.Data.DataModels;

internal sealed class SectorDataModel : IDataModel<Sector>
{
    public Guid Id { get; init; }
    public int Number { get; init; }
    public ICollection<PalletSpaceDataModel> PalletSpaces { get; init; }
    public ICollection<WarehousemanDataModel> Warehousemen { get; init; }

    private SectorDataModel(
        Guid id,
        int number,
        ICollection<PalletSpaceDataModel> palletSpaces,
        ICollection<WarehousemanDataModel> warehousemen)
    {
        Id = id;
        Number = number;
        PalletSpaces = palletSpaces;
        Warehousemen = warehousemen;
    }

    public Sector ToDomainModel()
    {
        return null;
    }
}