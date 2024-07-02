using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Warehousemen.AddWarehouseman;

internal sealed class AddWarehousemanCommandHandler : ICommandHandler<AddWarehousemanCommand, WarehousemanModel>
{
    public async Task<Result<WarehousemanModel>> Handle(AddWarehousemanCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
