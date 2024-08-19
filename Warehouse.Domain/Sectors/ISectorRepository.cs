using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Sectors;

public interface ISectorRepository
{
    Task<Result<Sector>> AddAsync(Sector sector, CancellationToken cancellationToken);
    Result Remove(Sector sector);
    Task<Result<Sector>> GetBySectorNumberAsync(SectorNumber sectorNumber, CancellationToken cancellationToken);
    Task<Result<Sector>> GetByIdAsync(SectorId id, CancellationToken cancellationToken);

    Task<Result<List<Sector>>> GetAllDetailedAsync(CancellationToken cancellationToken);
    Task<Result<List<Sector>>> GetAllIncludePalletSpacesAsync(CancellationToken cancellationToken);
    Task<Result<List<Sector>>> GetAllAsync(CancellationToken cancellationToken);
}