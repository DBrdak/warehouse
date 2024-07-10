using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Utils;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class ClientRepository : Repository<Client, ClientId>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<Client>> GetByIdWithTransportsAsync(
        ClientId clientId,
        CancellationToken cancellationToken) =>
        Result.Create(
            await Table.Include(e => e.Transports)
                .FirstOrDefaultAsync(
                    e => e.Id == clientId,
                    cancellationToken),
            DataAccessErrors.NotFound<Client>());
}