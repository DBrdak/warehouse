using Warehouse.Domain.Drivers;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class DriverRepository : Repository<Driver, DriverId>, IDriverRepository
{
    public DriverRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}