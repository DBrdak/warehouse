using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.PalletSpaces.RemovePalletSpace;

internal sealed class RemovePalletSpaceCommandHandler : ICommandHandler<RemovePalletSpaceCommand>
{
    private readonly IPalletSpaceRepository _palletSpaceRepository;

    public RemovePalletSpaceCommandHandler(IPalletSpaceRepository palletSpaceRepository)
    {
        _palletSpaceRepository = palletSpaceRepository;
    }

    public async Task<Result> Handle(RemovePalletSpaceCommand request, CancellationToken cancellationToken)
    {
        var palletSpaceGetResult = await _palletSpaceRepository.GetByIdAsync(new PalletSpaceId(request.Id), cancellationToken);

        if (palletSpaceGetResult.IsFailure)
        {
            return palletSpaceGetResult.Error;
        }

        var palletSpace = palletSpaceGetResult.Value;

        return _palletSpaceRepository.Remove(palletSpace);
    }
}
