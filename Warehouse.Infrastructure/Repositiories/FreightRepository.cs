using Warehouse.Domain.Freights;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class FreightRepository : Repository<Freight, FreightId>, IFreightRepository
{
    public FreightRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}