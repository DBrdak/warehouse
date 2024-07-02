using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Drivers;

public interface IDriverRepository
{
    Task<Result<Driver>> AddAsync(Driver driver, CancellationToken cancellationToken);
    Result<Driver> Update(Driver driver);
    Result Remove(DriverId driverId);
}