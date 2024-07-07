using Warehouse.Application.Freights.Models;
using Warehouse.Application.Sectors.Models;
using Warehouse.Application.Shared.Models;
using Warehouse.Domain.PalletSpaces;

namespace Warehouse.Application.PalletSpaces.Models;

public sealed record PalletSpaceModel : BusinessModel<PalletSpace, PalletSpaceId>
{
    public int Number { get; init; }
    public int Shelf { get; init; }
    public int Rack { get; init; }
    public SectorModel Sector { get; init; }
    public IReadOnlyCollection<FreightModel> Freights { get; init; }

    private PalletSpaceModel(
        Guid id,
        int number,
        int shelf,
        int rack,
        SectorModel sector,
        IReadOnlyCollection<FreightModel> freights) : base(id)
    {
        Number = number;
        Shelf = shelf;
        Rack = rack;
        Sector = sector;
        Freights = freights;
    }

    public static PalletSpaceModel FromDomainModel<TCaller>(PalletSpace palletSpace) =>
        typeof(TCaller) switch
        {
            var callerType when callerType == typeof(SectorModel) => new(
                palletSpace.Id.Id,
                palletSpace.Number.Value,
                palletSpace.Shelf.Value,
                palletSpace.Rack.Value,
                null,
                palletSpace.Freights.Select(f => FreightModel.FromDomainModel<PalletSpaceModel>(f)).ToList()),
            var callerType when callerType == typeof(FreightModel) => new(
                palletSpace.Id.Id,
                palletSpace.Number.Value,
                palletSpace.Shelf.Value,
                palletSpace.Rack.Value,
                SectorModel.FromDomainModel<PalletSpaceModel>(palletSpace.Sector),
                null),
            _ => FromDomainModel(palletSpace)
        };

    public static PalletSpaceModel FromDomainModel(PalletSpace palletSpace) =>
        new(
            palletSpace.Id.Id,
            palletSpace.Number.Value,
            palletSpace.Shelf.Value,
            palletSpace.Rack.Value,
            SectorModel.FromDomainModel<PalletSpaceModel>(palletSpace.Sector),
            palletSpace.Freights.Select(f => FreightModel.FromDomainModel<PalletSpaceModel>(f)).ToList());
}