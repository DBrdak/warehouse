using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.PalletSpaces.AddPalletSpace;

internal sealed class AddPalletSpaceCommandHandler : ICommandHandler<AddPalletSpaceCommand, PalletSpaceModel>
{
    public async Task<Result<PalletSpaceModel>> Handle(AddPalletSpaceCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
