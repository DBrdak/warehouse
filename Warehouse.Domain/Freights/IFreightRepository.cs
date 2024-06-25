using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Domain.Freights;

public interface IFreightRepository
{
    Task<Result<Warehouseman>> AddAsync(Freight freight, CancellationToken cancellationToken);
    Task<Result<Warehouseman>> UpdateAsync(Freight freight, CancellationToken cancellationToken);
    Task<Result> RemoveAsync(FreightId freightId, CancellationToken cancellationToken);
}