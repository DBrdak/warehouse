using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Application.Warehousemen.UpdateWarehouseman;

internal sealed class UpdateWarehousemanCommandHandler : ICommandHandler<UpdateWarehousemanCommand, WarehousemanModel>
{
    private readonly IWarehousemanRepository _warehousemanRepository;
    private readonly ISectorRepository _sectorRepository;
    private UpdateWarehousemanCommand command;

    public UpdateWarehousemanCommandHandler(IWarehousemanRepository warehousemanRepository, ISectorRepository sectorRepository)
    {
        _warehousemanRepository = warehousemanRepository;
        _sectorRepository = sectorRepository;
        command = new();
    }

    public async Task<Result<WarehousemanModel>> Handle(UpdateWarehousemanCommand request, CancellationToken cancellationToken)
    {
        command = request;

        var warehousemanGetResult =
            await _warehousemanRepository.GetByIdAsync(new(command.Id), cancellationToken);

        if (warehousemanGetResult.IsFailure)
        {
            return warehousemanGetResult.Error;
        }

        var warehouseman = warehousemanGetResult.Value;

        var warehousemanUpdateResult = Result.Aggregate(
            await UpdateSector(warehouseman, cancellationToken),
            string.IsNullOrWhiteSpace(command.FirstName) ? Result.Success() : warehouseman.EditFirstName(command.FirstName),
            string.IsNullOrWhiteSpace(command.LastName) ? Result.Success() : warehouseman.EditFirstName(command.LastName),
            string.IsNullOrWhiteSpace(command.Position) ? Result.Success() : warehouseman.EditFirstName(command.Position));

        if (warehousemanUpdateResult.IsFailure)
        {
            return warehousemanUpdateResult.Error;
        }

        var updateResult = _warehousemanRepository.Update(warehouseman);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }

        warehouseman = updateResult.Value;

        return WarehousemanModel.FromDomainModel(warehouseman);
    }

    private async Task<Result> UpdateSector(Warehouseman warehouseman, CancellationToken cancellationToken)
    {
        if (command.SectorNumber is null)
        {
            return Result.Success();
        }

        var sectorGetResult = await _sectorRepository.GetBySectorNumberAsync(
            command.SectorNumber.Value,
            cancellationToken);

        if (sectorGetResult.IsFailure)
        {
            return sectorGetResult.Error;
        }

        warehouseman.MoveToSector(sectorGetResult.Value);

        return Result.Success();
    }
}
