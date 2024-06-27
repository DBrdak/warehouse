using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Clients;

public interface IClientRepository
{
    Task<Result<Client>> AddAsync(Client client, CancellationToken cancellationToken);
    Result<Client> Update(Client client);
    Task<Result> RemoveAsync(ClientId clientId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<Client>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<Client>> GetClientWithTransportsAsync(ClientId clientId, CancellationToken cancellationToken);
}