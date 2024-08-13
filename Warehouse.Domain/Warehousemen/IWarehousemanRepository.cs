using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Warehousemen;

public interface IWarehousemanRepository
{
    Task<Result<Warehouseman>> AddAsync(Warehouseman warehouseman, CancellationToken cancellationToken);
    Result<Warehouseman> Update(Warehouseman warehouseman);
    Task<Result<Warehouseman>> GetByIdAsync(WarehousemanId warehousemanId, CancellationToken cancellationToken);

    Task<Result<List<Warehouseman>>> GetAllDetailedAsync(CancellationToken cancellationToken);

    Task<Result<Warehouseman>> GetByIdNumberAsync(IdentificationNumber idNumber, CancellationToken cancellationToken);

    Result Remove(Warehouseman warehouseman); }