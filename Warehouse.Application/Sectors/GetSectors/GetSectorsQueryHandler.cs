using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Sectors.Models;
using Warehouse.Application.Warehousemen.Models;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Sectors.GetSectors;

internal sealed class GetSectorsQueryHandler : IQueryHandler<GetSectorsQuery, List<SectorModel>>
{
    private readonly ISectorRepository _sectorRepository;

    public GetSectorsQueryHandler(ISectorRepository sectorRepository)
    {
        _sectorRepository = sectorRepository;
    }

    public async Task<Result<List<SectorModel>>> Handle(GetSectorsQuery request, CancellationToken cancellationToken)
    {
        var sectorsGetResult = await _sectorRepository.GetAllDetailedAsync(cancellationToken);

        if (sectorsGetResult.IsFailure)
        {
            return sectorsGetResult.Error;
        }

        var sectors = sectorsGetResult.Value;

        return sectors.Select(SectorModel.FromDomainModel<WarehousemanModel>).ToList();
    }
}
