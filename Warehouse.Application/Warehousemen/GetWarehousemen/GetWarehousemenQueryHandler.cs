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

        return null;
    }
}