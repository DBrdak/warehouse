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
        var sectorGetResult =
            await _sectorRepository.GetBySectorNumberAsync(request.SectorNumber, cancellationToken);

        if (sectorGetResult.IsFailure)
        {
            return sectorGetResult.Error;
        }

        var secotr = sectorGetResult.Value;

        var warehousemanHireResult = WarehousemanService.HireWarehouseman(
            request.IdentificationNumber,
            request.FirstName,
            request.LastName,
            request.Position,
            secotr);

        if (warehousemanHireResult.IsFailure)
        {
            return warehousemanHireResult.Error;
        }

        var warehouseman = warehousemanHireResult.Value;

        var addResult = await _warehousemanRepository.AddAsync(warehouseman, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error;
        }

        warehouseman = addResult.Value;

        return WarehousemanModel.FromDomainModel(warehouseman);
    }
}
