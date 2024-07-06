using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Clients.RemoveClient;

internal sealed class RemoveClientCommandHandler : ICommandHandler<RemoveClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public RemoveClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public Task<Result> Handle(
        RemoveClientCommand request,
        CancellationToken cancellationToken) =>
        Task.Run(() => _clientRepository.Remove(new(request.ClientId)), cancellationToken);
}
