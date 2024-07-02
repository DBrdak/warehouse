using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Sectors;

public interface ISectorRepository
{
    Task<Result<Sector>> AddAsync(Sector sector, CancellationToken cancellationToken);
    Result<Sector> Update(Sector sector);
    Result Remove(SectorId sectorId);
}