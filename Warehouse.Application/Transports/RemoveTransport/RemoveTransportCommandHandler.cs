using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Transports.RemoveTransport;

internal sealed class RemoveTransportCommandHandler : ICommandHandler<RemoveTransportCommand>
{
    public async Task<Result> Handle(RemoveTransportCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
