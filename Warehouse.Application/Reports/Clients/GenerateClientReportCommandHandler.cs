using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Reports.Clients;

internal sealed class GenerateClientReportCommandHandler : ICommandHandler<GenerateClientReportCommand>
{
    public async Task<Result> Handle(GenerateClientReportCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
