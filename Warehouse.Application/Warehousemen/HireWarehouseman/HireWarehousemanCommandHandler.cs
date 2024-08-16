using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Application.Warehousemen.HireWarehouseman;

internal sealed class HireWarehousemanCommandHandler : ICommandHandler<HireWarehousemanCommand, WarehousemanModel>
{
    private readonly IWarehousemanRepository _warehousemanRepository;
    private readonly ISectorRepository _sectorRepository;

    public HireWarehousemanCommandHandler(IWarehousemanRepository warehousemanRepository, ISectorRepository sectorRepository)
    {
        _warehousemanRepository = warehousemanRepository;
        _sectorRepository = sectorRepository;
    }

    public async Task<Result<WarehousemanModel>> Handle(HireWarehousemanCommand request, CancellationToken cancellationToken)
    {
        var sectorNumberCreateResult = SectorNumber.Create(request.SectorNumber);

        if (sectorNumberCreateResult.IsFailure)
        {
            return sectorNumberCreateResult.Error;
        }

        var sectorNumber = sectorNumberCreateResult.Value;

        var sectorGetResult =
            await _sectorRepository.GetBySectorNumberAsync(sectorNumber, cancellationToken);

        if (sectorGetResult.IsFailure)
        {
            return sectorGetResult.Error;
        }

        var sector = sectorGetResult.Value;

        var warehousemanHireResult = WarehousemanService.HireWarehouseman(
            request.IdentificationNumber,
            request.FirstName,
            request.LastName,
            request.Position,
            sector);

        if (warehousemanHireResult.IsFailure)
        {
            return warehousemanHireResult.Error;
        }

        var warehouseman = warehousemanHireResult.Value;

        var warehousemanwithSameIdGetResult = await _warehousemanRepository.GetByIdNumberAsync(
            warehouseman.IdentificationNumber,
            cancellationToken);

        if (!warehousemanwithSameIdGetResult.IsFailure)
        {
            return new Error("Magazynier z tym numerem identyfikacyjnym ju¿ istnieje");
        }

        var addResult = await _warehousemanRepository.AddAsync(warehouseman, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error;
        }

        warehouseman = addResult.Value;

        return WarehousemanModel.FromDomainModel(warehouseman);
    }
}
