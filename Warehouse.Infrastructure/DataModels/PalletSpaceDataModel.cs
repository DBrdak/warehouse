namespace Warehouse.Infrastructure.DataModels;

internal sealed class PalletSpaceDataModel
{
    public Guid Id { get; init; }
    public Guid SectorId { get; init; }
    public int Number { get; init; }
    public int Shelf { get; init; }
    public int Rack { get; init; }
    public SectorDataModel Sector { get; init; }
    public ICollection<FreightDataModel> Freights { get; init; }

    public PalletSpaceDataModel(
        Guid id,
        Guid sectorId,
        int number,
        int shelf,
        int rack,
        ICollection<FreightDataModel> freights,
        SectorDataModel sector)
    {
        Id = id;
        SectorId = sectorId;
        Number = number;
        Shelf = shelf;
        Rack = rack;
        Freights = freights;
        Sector = sector;
    }
}