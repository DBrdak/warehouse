using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Warehousemen.UpdateWarehouseman;

internal sealed class UpdateWarehousemanCommandHandler : ICommandHandler<UpdateWarehousemanCommand, WarehousemanModel>
{
    public async Task<Result<WarehousemanModel>> Handle(UpdateWarehousemanCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
