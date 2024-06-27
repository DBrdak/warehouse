using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure;

namespace Warehouse.Persistance.Repositiories;

internal sealed class ClientRepository : Repository<Client, ClientId>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<Client>> GetClientWithTransportsAsync(ClientId clientId, CancellationToken cancellationToken)
    {
        return null;
    }
}