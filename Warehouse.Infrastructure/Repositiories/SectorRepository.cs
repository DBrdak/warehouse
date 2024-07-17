using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Freights;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;
using Warehouse.Domain.Warehousemen;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Utils;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class SectorRepository : Repository<Sector, SectorId>, ISectorRepository
{
    public SectorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<Result<Sector>> GetBySectorNumberAsync(int sectorNumber, CancellationToken cancellationToken) =>
        await Table.FirstOrDefaultAsync(
            e => e.Number.Value == sectorNumber,
            cancellationToken) ??
        Result.Failure<Sector>(DataAccessErrors.NotFound<Sector>());

    public async Task<Result<List<Sector>>> GetAllIncludePalletSpacesThenFreightsWithExportAsync(
        CancellationToken cancellationToken) =>
        await Table
            .Include(e => e.PalletSpaces)
            .ThenInclude(e => e.Freights)
            .ThenInclude(e => e.Export)
            .ToListAsync(cancellationToken);
}