using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Warehousemen;

public interface IWarehousemanRepository
{
    Task<Result<Warehouseman>> AddAsync(Warehouseman warehouseman, CancellationToken cancellationToken);
    Result<Warehouseman> Update(Warehouseman warehouseman);
    Task<Result<Warehouseman>> GetByIdAsync(WarehousemanId warehousemanId, CancellationToken cancellationToken);

    Task<Result<IEnumerable<Warehouseman>>> GetAllDetailedAsync(CancellationToken cancellationToken);
}