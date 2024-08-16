using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Sectors.RemoveSector;

internal sealed class RemoveSectorCommandHandler : ICommandHandler<RemoveSectorCommand>
{
    private readonly ISectorRepository _sectorRepository;

    public RemoveSectorCommandHandler(ISectorRepository sectorRepository)
    {
        _sectorRepository = sectorRepository;
    }

    public async Task<Result> Handle(RemoveSectorCommand request, CancellationToken cancellationToken)
    {
        var sectorGetResult = await _sectorRepository.GetByIdAsync(new SectorId(request.Id), cancellationToken);

        if (sectorGetResult.IsFailure)
        {
            return sectorGetResult.Error;
        }

        var sector = sectorGetResult.Value;

        return _sectorRepository.Remove(sector);
    }
}
