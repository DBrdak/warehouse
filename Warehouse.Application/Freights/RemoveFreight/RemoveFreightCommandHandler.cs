using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Freights.RemoveFreight;

internal sealed class RemoveFreightCommandHandler : ICommandHandler<RemoveFreightCommand>
{
    public async Task<Result> Handle(RemoveFreightCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
