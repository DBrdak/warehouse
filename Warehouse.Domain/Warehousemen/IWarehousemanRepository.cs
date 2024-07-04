using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Warehousemen;

public interface IWarehousemanRepository
{
    Task<Result<Warehouseman>> AddAsync(Warehouseman warehouseman, CancellationToken cancellationToken);
    Result<Warehouseman> Update(Warehouseman warehouseman);
    Result Remove(WarehousemanId warehousemanId);

    Task<Result<Warehouseman>> GetByIdAsync(WarehousemanId warehousemanId, CancellationToken cancellationToken);
}