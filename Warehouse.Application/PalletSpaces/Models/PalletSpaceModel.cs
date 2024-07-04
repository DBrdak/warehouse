using Warehouse.Application.Freights.Models;
using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Freights;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Sectors;

namespace Warehouse.Application.PalletSpaces.Models;

public sealed record PalletSpaceModel
{
    public int Number { get; init; }
    public int Shelf { get; init; }
    public int Rack { get; init; }
    public SectorModel Sector { get; init; }
    public IReadOnlyCollection<FreightModel> Freights { get; init; }

    private PalletSpaceModel(
        int number,
        int shelf,
        int rack,
        SectorModel sector,
        IReadOnlyCollection<FreightModel> freights)
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
                palletSpace.Number.Value,
                palletSpace.Shelf.Value,
                palletSpace.Rack.Value,
                null,
                palletSpace.Freights.Select(f => FreightModel.FromDomainModel<PalletSpaceModel>(f)).ToList()),
            var callerType when callerType == typeof(FreightModel) => new(
                palletSpace.Number.Value,
                palletSpace.Shelf.Value,
                palletSpace.Rack.Value,
                SectorModel.FromDomainModel<PalletSpaceModel>(palletSpace.Sector),
                null),
            _ => FromDomainModel(palletSpace)
        };

    public static PalletSpaceModel FromDomainModel(PalletSpace palletSpace) =>
        new(
            palletSpace.Number.Value,
            palletSpace.Shelf.Value,
            palletSpace.Rack.Value,
            SectorModel.FromDomainModel<PalletSpaceModel>(palletSpace.Sector),
            palletSpace.Freights.Select(f => FreightModel.FromDomainModel<PalletSpaceModel>(f)).ToList());
}