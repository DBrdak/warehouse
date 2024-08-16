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

    public async Task<Result> Handle(
        RemoveClientCommand request,
        CancellationToken cancellationToken)
    {
        var clientGetResult = await _clientRepository.GetByIdAsync(new ClientId(request.ClientId), cancellationToken);

        if (clientGetResult.IsFailure)
        {
            return clientGetResult.Error;
        }

        var client = clientGetResult.Value;

        return _clientRepository.Remove(client);
    }
}
