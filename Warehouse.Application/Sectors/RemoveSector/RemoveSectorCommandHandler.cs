using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Sectors.RemoveSector;

internal sealed class RemoveSectorCommandHandler : ICommandHandler<RemoveSectorCommand>
{
    public async Task<Result> Handle(RemoveSectorCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
