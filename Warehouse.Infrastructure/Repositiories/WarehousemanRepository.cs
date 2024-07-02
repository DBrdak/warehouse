using Warehouse.Domain.Warehousemen;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class WarehousemanRepository : Repository<Warehouseman, WarehousemanId>, IWarehousemanRepository
{
    public WarehousemanRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}