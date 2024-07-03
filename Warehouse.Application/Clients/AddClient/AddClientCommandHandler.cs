using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Clients.Models;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Clients.AddClient;

internal sealed class AddClientCommandHandler : ICommandHandler<AddClientCommand, ClientModel>
{
    private readonly IClientRepository _clientRepository;

    public AddClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Result<ClientModel>> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        var clientCreateResult = Client.Create(request.Nip, request.Name);

        if (clientCreateResult.IsFailure)
        {
            return clientCreateResult.Error;
        }

        var client = clientCreateResult.Value;

        var addResult = await _clientRepository.AddAsync(client, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error;
        }

        client = addResult.Value;

        return ClientModel.FromDomainModel(client);
    }
}
