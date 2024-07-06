using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Application.Warehousemen.FireWarehouseman;

internal sealed class FireWarehousemanCommandHandler : ICommandHandler<FireWarehousemanCommand>
{
    private readonly IWarehousemanRepository _warehousemanRepository;

    public FireWarehousemanCommandHandler(IWarehousemanRepository warehousemanRepository)
    {
        _warehousemanRepository = warehousemanRepository;
    }

    public async Task<Result> Handle(FireWarehousemanCommand request, CancellationToken cancellationToken)
    {
        var warehousemanGetResult =
            await _warehousemanRepository.GetByIdAsync(new(request.Id), cancellationToken);

        if (warehousemanGetResult.IsFailure)
        {
            return warehousemanGetResult.Error;
        }

        var warehouseman = warehousemanGetResult.Value;

        warehouseman.Fire();

        var updateResult = _warehousemanRepository.Update(warehouseman);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }

        warehouseman = updateResult.Value;

        return Result.Success();
    }
}
