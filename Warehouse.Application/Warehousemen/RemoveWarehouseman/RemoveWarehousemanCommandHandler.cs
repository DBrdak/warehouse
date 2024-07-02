using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Warehousemen.RemoveWarehouseman;

internal sealed class RemoveWarehousemanCommandHandler : ICommandHandler<RemoveWarehousemanCommand>
{
    public async Task<Result> Handle(RemoveWarehousemanCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
