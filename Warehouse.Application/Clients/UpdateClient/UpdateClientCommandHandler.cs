using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Clients.Models;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Clients.UpdateClient;

internal sealed class UpdateClientCommandHandler : ICommandHandler<UpdateClientCommand, ClientModel>
{
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Result<ClientModel>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var clientGetResult = await _clientRepository.GetByIdAsync(new(request.Id), cancellationToken);

        if (clientGetResult.IsFailure)
        {
            return clientGetResult.Error;
        }

        var client = clientGetResult.Value;

        var editResult = request switch
        {
            _ when request.NewName is not null => client.EditName(request.NewName),
            _ when request.NewNip is not null => client.EditNIP(request.NewNip),
            _ => UpdateClientErrors.InvalidRequest
        };

        if (editResult.IsFailure)
        {
            return editResult.Error;
        }

        var updateResult = _clientRepository.Update(client);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }

        client = updateResult.Value;

        return ClientModel.FromDomainModel(client);
    }
}
