using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data.DataModels.Shared;

namespace Warehouse.Infrastructure.Data.DataModels;

internal sealed class PalletSpaceDataModel : IDataModel<PalletSpace>
{
    public Guid Id { get; init; }
    public Guid SectorId { get; init; }
    public int Number { get; init; }
    public int Shelf { get; init; }
    public int Rack { get; init; }
    public SectorDataModel Sector { get; init; }
    public ICollection<FreightDataModel> Freights { get; init; }

    private PalletSpaceDataModel(
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

    public PalletSpace ToDomainModel()
    {
        return null;
    }
}