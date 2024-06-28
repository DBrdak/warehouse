using Warehouse.Domain.Clients;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class ClientRepository : Repository<Client, ClientId>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}