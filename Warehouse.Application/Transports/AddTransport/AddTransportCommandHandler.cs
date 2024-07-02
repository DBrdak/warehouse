using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Transports.AddTransport;

internal sealed class AddTransportCommandHandler : ICommandHandler<AddTransportCommand, TransportModel>
{
    public async Task<Result<TransportModel>> Handle(AddTransportCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
