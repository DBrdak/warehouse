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

    public async Task<Result> Handle(RemoveSectorCommand request, CancellationToken cancellationToken) =>
        await Task.Run(() => _sectorRepository.Remove(new(request.Id)), cancellationToken);
}
