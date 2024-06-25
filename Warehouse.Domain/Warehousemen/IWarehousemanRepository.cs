using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Warehousemen;

public interface IWarehousemanRepository
{
    Task<Result<IEnumerable<Warehouseman>>> GetByIdentificationNumberAsync(
        IdentificationNumber idNumber,
        CancellationToken cancellationToken);

    Task<Result<Warehouseman>> AddAsync(Warehouseman warehouseman, CancellationToken cancellationToken);
    Task<Result<Warehouseman>> UpdateAsync(Warehouseman warehouseman, CancellationToken cancellationToken);
    Task<Result> RemoveAsync(WarehousemanId warehousemanId, CancellationToken cancellationToken);
}