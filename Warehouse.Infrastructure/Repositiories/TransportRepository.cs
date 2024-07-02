using Warehouse.Domain.Transports;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class TransportRepository : Repository<Transport, TransportId>, ITransportRepository
{
    public TransportRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}