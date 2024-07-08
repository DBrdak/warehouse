using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Transports.RemoveTransport;

internal sealed class RemoveTransportCommandHandler : ICommandHandler<RemoveTransportCommand>
{
    private readonly ITransportRepository _transportRepository;

    public RemoveTransportCommandHandler(ITransportRepository transportRepository)
    {
        _transportRepository = transportRepository;
    }

    public async Task<Result> Handle(RemoveTransportCommand request, CancellationToken cancellationToken)
    {
        var transportGetResult = await _transportRepository.GetByIdAsync(new TransportId(request.Id), cancellationToken);

        if (transportGetResult.IsFailure)
        {
            return transportGetResult.Error;
        }

        var transport = transportGetResult.Value;

        return _transportRepository.Remove(transport);
    }
}
