using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Sectors.AddSector;

internal sealed class AddSectorCommandHandler : ICommandHandler<AddSectorCommand, SectorModel>
{
    private readonly ISectorRepository _sectorRepository;
    private readonly IPalletSpaceRepository _palletSpaceRepository;

    public AddSectorCommandHandler(ISectorRepository sectorRepository, IPalletSpaceRepository palletSpaceRepository)
    {
        _sectorRepository = sectorRepository;
        _palletSpaceRepository = palletSpaceRepository;
    }

    public async Task<Result<SectorModel>> Handle(AddSectorCommand request, CancellationToken cancellationToken)
    {
        var sectorCreateResult = Sector.Create(request.SectorNumber);

        if (sectorCreateResult.IsFailure)
        {
            return sectorCreateResult.Error;
        }

        var sector = sectorCreateResult.Value;
        List<PalletSpace> palletSpaces = [];
        
        var palletSpacesCreateResult = CreatePalletSpaces(request, sector, ref palletSpaces);

        if (palletSpacesCreateResult.IsFailure)
        {
            return palletSpacesCreateResult.Error;
        }

        var secotrAddResult = await _sectorRepository.AddAsync(sector, cancellationToken);

        if (secotrAddResult.IsFailure)
        {
            return secotrAddResult.Error;
        }

        sector = secotrAddResult.Value;

        var palletSpacesAddResult =
            await _palletSpaceRepository.AddRangeAsync(palletSpaces, cancellationToken);

        if (palletSpacesAddResult.IsFailure)
        {
            return palletSpacesAddResult.Error;
        }

        return SectorModel.FromDomainModel(sector);
    }

    private static Result CreatePalletSpaces(
        AddSectorCommand request,
        Sector sector,
        ref List<PalletSpace> palletSpaces)
    {
        foreach (var rack in request.Racks)
        {
            foreach (var shelf in rack.Shelfs)
            {
                for (var i = 1; i <= shelf.PalletSpaceCount; i++)
                {
                    var palletSpaceCreateResult = PalletSpaceService.CreatePalletSpace(i, shelf.ShelfNumber, rack.RackNumber, sector);

                    if (palletSpaceCreateResult.IsFailure)
                    {
                        return palletSpaceCreateResult.Error;
                    }

                    palletSpaces.Add(palletSpaceCreateResult.Value);
                }
            }
        }

        return Result.Success();
    }
}
