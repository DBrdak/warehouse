using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Clients.Models;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Clients.GetAllClients;

internal sealed class GetAllClientsQueryHandler : IQueryHandler<GetAllClientsQuery, List<ClientModel>>
{
    private readonly IClientRepository _clientRepository;

    public GetAllClientsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Result<List<ClientModel>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var clientsGetResult = await _clientRepository.GetAllAsync(cancellationToken);

        if (clientsGetResult.IsFailure)
        {
            return clientsGetResult.Error;
        }

        var clients = clientsGetResult.Value;

        return clients.Select(ClientModel.FromDomainModel).ToList();
    }
}
