using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.PalletSpaces.AddPalletSpace;

internal sealed class AddPalletSpaceCommandHandler : ICommandHandler<AddPalletSpaceCommand, PalletSpaceModel>
{
    private readonly IPalletSpaceRepository _palletSpaceRepository;
    private readonly ISectorRepository _sectorRepository;

    public AddPalletSpaceCommandHandler(
        IPalletSpaceRepository palletSpaceRepository,
        ISectorRepository sectorRepository)
    {
        _palletSpaceRepository = palletSpaceRepository;
        _sectorRepository = sectorRepository;
    }

    public async Task<Result<PalletSpaceModel>> Handle(
        AddPalletSpaceCommand request,
        CancellationToken cancellationToken)
    {
        var sectorGetResult = await _sectorRepository.GetBySectorNumberAsync(request.SectorNumber, cancellationToken);

        if (sectorGetResult.IsFailure)
        {
            return sectorGetResult.Error;
        }

        var sector = sectorGetResult.Value;

        var palletSpaceCreateResult = PalletSpaceService.CreatePalletSpace(
            request.Number,
            request.Shelf,
            request.Rack,
            sector);

        if (palletSpaceCreateResult.IsFailure)
        {
            return palletSpaceCreateResult.Error;
        }

        var palletSpace = palletSpaceCreateResult.Value;

        var updateResult = await _palletSpaceRepository.AddAsync(palletSpace, cancellationToken);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }

        palletSpace = updateResult.Value;

        return PalletSpaceModel.FromDomainModel(palletSpace);
    }
}
