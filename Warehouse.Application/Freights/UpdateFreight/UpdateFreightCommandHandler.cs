using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Freights.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Freights.UpdateFreight;

internal sealed class UpdateFreightCommandHandler : ICommandHandler<UpdateFreightCommand, FreightModel>
{
    public async Task<Result<FreightModel>> Handle(UpdateFreightCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
