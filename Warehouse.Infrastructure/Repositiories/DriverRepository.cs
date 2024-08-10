using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Utils;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class DriverRepository : Repository<Driver, DriverId>, IDriverRepository
{
    public DriverRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<Result<Driver>> GetByIdDetailedAsync(
        DriverId entityId,
        CancellationToken cancellationToken) =>
        Result.Create(
            await Table
                .Include(c => c.Transports)
                .ThenInclude(t => t.Warehouseman)
                .Include(c => c.Transports)
                .ThenInclude(t => t.Client)
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(
                    e => e.Id == entityId,
                    cancellationToken),
            DataAccessErrors.NotFound<Driver>());
}