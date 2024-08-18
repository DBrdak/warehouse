using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class PalletSpaceRepository : Repository<PalletSpace, PalletSpaceId>, IPalletSpaceRepository
{
    public PalletSpaceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<PalletSpace>> GetByDataAsync(
        PalletSpaceNumber palletSpaceNumber,
        Shelf shelfNumber,
        Rack rackNumber,
        SectorNumber sectorNumber) =>
        await Table
            .Include(ps => ps.Sector)
            .Include(ps => ps.Freights)
            .FirstOrDefaultAsync(
                ps =>
                    ps.Number == palletSpaceNumber &&
                    ps.Shelf == shelfNumber &&
                    ps.Rack == rackNumber &&
                    ps.Sector.Number == sectorNumber);
}