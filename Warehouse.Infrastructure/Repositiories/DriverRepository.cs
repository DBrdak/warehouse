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


    public async Task<Result<Driver>> GetByIdWithTransportsAsync(
        DriverId entityId,
        CancellationToken cancellationToken) =>
        Result.Create(
            await Table.Include(e => e.Transports)
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(
                    e => e.Id == entityId,
                    cancellationToken),
            DataAccessErrors.NotFound<Driver>());
}