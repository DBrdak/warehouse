using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.PalletSpaces.UpdatePalletSpace;

internal sealed class UpdatePalletSpaceCommandHandler : ICommandHandler<UpdatePalletSpaceCommand, PalletSpaceModel>
{
    public async Task<Result<PalletSpaceModel>> Handle(UpdatePalletSpaceCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
