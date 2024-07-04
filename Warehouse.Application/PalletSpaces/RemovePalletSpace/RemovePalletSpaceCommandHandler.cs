using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.PalletSpaces.Models;
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

    public async Task<Result> Handle(RemovePalletSpaceCommand request, CancellationToken cancellationToken) =>
        await Task.Run(() => _palletSpaceRepository.Remove(new(request.Id)), cancellationToken);
}
