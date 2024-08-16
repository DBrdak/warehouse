using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Drivers;

public interface IDriverRepository
{
    Task<Result<Driver>> AddAsync(Driver driver, CancellationToken cancellationToken);
    Result<Driver> Update(Driver driver);
    Result Remove(Driver driver);

    Task<Result<Driver>> GetByIdAsync(DriverId driverId, CancellationToken cancellationToken);

    Task<Result<List<Driver>>> GetAllAsync(CancellationToken cancellationToken);

    Task<Result<Driver>> GetByIdDetailedAsync(DriverId entityId, CancellationToken cancellationToken);
}