using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Drivers;

public interface IDriverRepository
{
    Task<Result<Driver>> AddAsync(Driver driver, CancellationToken cancellationToken);
    Task<Result<Driver>> UpdateAsync(Driver driver, CancellationToken cancellationToken);

    Task<Result> RemoveAsync(DriverId driverId, CancellationToken cancellationToken);

    Result<IEnumerable<Driver>> GetAll();

    Task<Result<Driver>> GetDriverWithTransportsAsync(DriverId driverId, CancellationToken cancellationToken);
}