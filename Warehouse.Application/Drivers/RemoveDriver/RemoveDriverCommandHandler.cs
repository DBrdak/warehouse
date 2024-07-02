using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Drivers.RemoveDriver;

internal sealed class RemoveDriverCommandHandler : ICommandHandler<RemoveDriverCommand>
{
    public async Task<Result> Handle(RemoveDriverCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
