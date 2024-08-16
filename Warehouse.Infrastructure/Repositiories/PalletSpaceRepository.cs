using Warehouse.Domain.PalletSpaces;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class PalletSpaceRepository : Repository<PalletSpace, PalletSpaceId>, IPalletSpaceRepository
{
    public PalletSpaceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}