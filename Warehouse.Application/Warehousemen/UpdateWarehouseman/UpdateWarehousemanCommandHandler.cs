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

    public UpdateWarehousemanCommandHandler(IWarehousemanRepository warehousemanRepository, ISectorRepository sectorRepository)
    {
        _warehousemanRepository = warehousemanRepository;
        _sectorRepository = sectorRepository;
    }

    public async Task<Result<WarehousemanModel>> Handle(UpdateWarehousemanCommand request, CancellationToken cancellationToken)
    {
        var warehousemanGetResult =
            await _warehousemanRepository.GetByIdAsync(new(request.Id), cancellationToken);

        if (warehousemanGetResult.IsFailure)
        {
            return warehousemanGetResult.Error;
        }

        var warehouseman = warehousemanGetResult.Value;

        var warehousemanUpdateResult = Result.Aggregate(
            await UpdateSector(request.SectorNumber, warehouseman, cancellationToken),
            string.IsNullOrWhiteSpace(request.FirstName) ? Result.Success() : warehouseman.EditFirstName(request.FirstName),
            string.IsNullOrWhiteSpace(request.LastName) ? Result.Success() : warehouseman.EditLastName(request.LastName),
            string.IsNullOrWhiteSpace(request.Position) ? Result.Success() : warehouseman.EditPosition(request.Position));

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

    private async Task<Result> UpdateSector(
        int? plainSectorNumber,
        Warehouseman warehouseman,
        CancellationToken cancellationToken)
    {
        if (plainSectorNumber is null)
        {
            return Result.Success();
        }

        var sectorNumberCreateResult = SectorNumber.Create(plainSectorNumber.Value);

        if (sectorNumberCreateResult.IsFailure)
        {
            return sectorNumberCreateResult.Error;
        }

        var sectorNumber = sectorNumberCreateResult.Value;

        var sectorGetResult = await _sectorRepository.GetBySectorNumberAsync(
            sectorNumber,
            cancellationToken);

        if (sectorGetResult.IsFailure)
        {
            return sectorGetResult.Error;
        }

        warehouseman.MoveToSector(sectorGetResult.Value);

        return Result.Success();
    }
}
