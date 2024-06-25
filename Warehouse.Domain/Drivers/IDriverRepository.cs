using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Drivers;

internal interface IDriverRepository
{
    Task<Result<Driver>> AddAsync(Driver driver, CancellationToken cancellationToken);
    Task<Result<Driver>> UpdateAsync(Driver driver, CancellationToken cancellationToken);

    Task<Result> RemoveAsync(DriverId driverId, CancellationToken cancellationToken);
}