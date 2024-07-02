using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Freights.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Freights.AddFreight;

internal sealed class AddFreightCommandHandler : ICommandHandler<AddFreightCommand, FreightModel>
{
    public async Task<Result<FreightModel>> Handle(AddFreightCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
