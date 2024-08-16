using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Utils;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class ClientRepository : Repository<Client, ClientId>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<Client>> GetByIdDetailedAsync(
        ClientId clientId,
        CancellationToken cancellationToken) =>
        Result.Create(
            await Table
                .Include(c => c.Transports)
                .ThenInclude(t => t.Warehouseman)
                .Include(c => c.Transports)
                .ThenInclude(t => t.Driver)
                .FirstOrDefaultAsync(
                    e => e.Id == clientId,
                    cancellationToken),
            DataAccessErrors.NotFound<Client>());
}