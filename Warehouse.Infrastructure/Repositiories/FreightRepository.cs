using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Freights;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class FreightRepository : Repository<Freight, FreightId>, IFreightRepository
{
    public FreightRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<List<Freight>>> GetManyByIdAsync(
        List<FreightId> freightsId,
        CancellationToken cancellationToken) =>
        await Table
            .Include(f => f.Export)
            .Include(f => f.Import)
            .Include(f => f.PalletSpace)
            .Where(f => freightsId.Any(fid => fid == f.Id))
            .ToListAsync(cancellationToken);

    public async Task<Result<List<Freight>>>
        GetAllImportsDetailedAsync(CancellationToken cancellationToken) =>
        await Table
            .Include(f => f.Import)
            .ThenInclude(i => i.Client)
            .Include(f => f.Import)
            .ThenInclude(i => i.Warehouseman)
            .Include(f => f.Import)
            .ThenInclude(i => i.Driver)
            .Include(f => f.PalletSpace)
            .ThenInclude(ps => ps.Sector)
            .Where(f => f.Export == null)
            .ToListAsync(cancellationToken);
}