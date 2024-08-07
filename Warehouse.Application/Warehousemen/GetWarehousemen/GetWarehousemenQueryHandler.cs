using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Application.Warehousemen.GetWarehousemen;

internal class GetWarehousemenQueryHandler : IQueryHandler<GetWarehousemenQuery, IEnumerable<WarehousemanModel>>
{
    private readonly IWarehousemanRepository _warehousemanRepository;

    public GetWarehousemenQueryHandler(IWarehousemanRepository warehousemanRepository)
    {
        _warehousemanRepository = warehousemanRepository;
    }

    public async Task<Result<IEnumerable<WarehousemanModel>>> Handle(GetWarehousemenQuery request, CancellationToken cancellationToken)
    {
        var getWarehousemenResult = await _warehousemanRepository.GetAllDetailedAsync(cancellationToken);

        if (getWarehousemenResult.IsFailure)
        {
            return getWarehousemenResult.Error;
        }

        var warehousemen = getWarehousemenResult.Value;

        return Result.Create(warehousemen.Select(WarehousemanModel.FromDomainModel));
    }
}