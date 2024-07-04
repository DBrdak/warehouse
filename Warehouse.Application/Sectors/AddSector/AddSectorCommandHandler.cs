using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Sectors.AddSector;

internal sealed class AddSectorCommandHandler : ICommandHandler<AddSectorCommand, SectorModel>
{
    private readonly ISectorRepository _sectorRepository;

    public AddSectorCommandHandler(ISectorRepository sectorRepository)
    {
        _sectorRepository = sectorRepository;
    }

    public async Task<Result<SectorModel>> Handle(AddSectorCommand request, CancellationToken cancellationToken)
    {
        var sectorCreateResult = Sector.Create(request.Number);

        if (sectorCreateResult.IsFailure)
        {
            return sectorCreateResult.Error;
        }

        var sector = sectorCreateResult.Value;

        var addResult = await _sectorRepository.AddAsync(sector, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error;
        }

        sector = addResult.Value;

        return SectorModel.FromDomainModel(sector);
    }
}
