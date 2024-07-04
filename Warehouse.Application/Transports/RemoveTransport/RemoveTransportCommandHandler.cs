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

    public async Task<Result> Handle(RemoveTransportCommand request, CancellationToken cancellationToken) =>
        await Task.Run(() => _transportRepository.Remove(new(request.Id)), cancellationToken);
}
