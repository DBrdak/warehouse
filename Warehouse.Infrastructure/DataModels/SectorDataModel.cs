namespace Warehouse.Infrastructure.DataModels;

internal sealed class SectorDataModel
{
    public Guid Id { get; init; }
    public int Number { get; init; }
    public ICollection<PalletSpaceDataModel> PalletSpaces { get; init; }
    public ICollection<WarehousemanDataModel> Warehousemen { get; init; }

    public SectorDataModel(
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
}