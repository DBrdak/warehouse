using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.PalletSpaces.RemovePalletSpace;

internal sealed class RemovePalletSpaceCommandHandler : ICommandHandler<RemovePalletSpaceCommand>
{
    public async Task<Result> Handle(RemovePalletSpaceCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
