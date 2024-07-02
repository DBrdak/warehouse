using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Sectors.AddSector;

internal sealed class AddSectorCommandHandler : ICommandHandler<AddSectorCommand, SectorModel>
{
    public async Task<Result<SectorModel>> Handle(AddSectorCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
