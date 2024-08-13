using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Utils;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class SectorRepository : Repository<Sector, SectorId>, ISectorRepository
{
    public SectorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<Result<Sector>> GetBySectorNumberAsync(SectorNumber sectorNumber, CancellationToken cancellationToken) =>
        await Table
            .Include(s => s.Warehousemen)
            .FirstOrDefaultAsync(
            e => e.Number == sectorNumber,
            cancellationToken) ??
        Result.Failure<Sector>(DataAccessErrors.NotFound<Sector>());

    public async Task<Result<List<Sector>>> GetAllDetailedAsync(
        CancellationToken cancellationToken) =>
        await Table
            .OrderBy(s => s.Number)
            .Include(e => e.PalletSpaces
                .OrderBy(p => p.Rack)
                .ThenBy(p => p.Shelf)
                .ThenBy(p => p.Number))
            .ThenInclude(ps => ps.Freights)
            .ThenInclude(f => f.Export)
            //.ThenInclude(t => t.Freights)
            .ToListAsync(cancellationToken);
}