using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Sectors.UpdateSector;

internal sealed class UpdateSectorCommandHandler : ICommandHandler<UpdateSectorCommand, SectorModel>
{
    public async Task<Result<SectorModel>> Handle(UpdateSectorCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
