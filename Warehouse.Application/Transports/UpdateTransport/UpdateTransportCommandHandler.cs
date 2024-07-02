using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Transports.UpdateTransport;

internal sealed class UpdateTransportCommandHandler : ICommandHandler<UpdateTransportCommand, TransportModel>
{
    public async Task<Result<TransportModel>> Handle(UpdateTransportCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
