using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Clients;

public interface IClientRepository
{
    Task<Result<Client>> AddAsync(Client client, CancellationToken cancellationToken);
    Task<Result<Client>> UpdateAsync(Client client, CancellationToken cancellationToken);
    Task<Result> RemoveAsync(ClientId clientId, CancellationToken cancellationToken);
}