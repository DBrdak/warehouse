using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Drivers;

public interface IDriverRepository
{
    Task<Result<Driver>> AddAsync(Driver driver, CancellationToken cancellationToken);
    Task<Result<Driver>> UpdateAsync(Driver driver, CancellationToken cancellationToken);

    Task<Result> RemoveAsync(DriverId driverId, CancellationToken cancellationToken);

    Task<Result<IEnumerable<Driver>>> GetAllAsync(CancellationToken cancellationToken);

    Task<Result<Driver>> GetDriverWithTransportsAsync(DriverId driverId, CancellationToken cancellationToken);
}