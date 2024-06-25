using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Sectors;

public interface ISectorRepository
{
    Task<Result<Sector>> AddAsync(Sector sector, CancellationToken cancellationToken);
    Task<Result<Sector>> UpdateAsync(Sector sector, CancellationToken cancellationToken);
    Task<Result> RemoveAsync(SectorId sectorId, CancellationToken cancellationToken);
}