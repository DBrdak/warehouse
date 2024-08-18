using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Freights;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Freights.ReleaseFreight;

internal sealed class ReleaseFreightCommandHandler : ICommandHandler<ReleaseFreightCommand>
{
    private readonly IFreightRepository _freightRepository;
    private readonly ITransportRepository _transportRepository;

    public ReleaseFreightCommandHandler(ITransportRepository transportRepository, IFreightRepository freightRepository)
    {
        _transportRepository = transportRepository;
        _freightRepository = freightRepository;
    }

    public async Task<Result> Handle(ReleaseFreightCommand request, CancellationToken cancellationToken)
    {
        var freightsId = request.Freights.Select(f => new FreightId(f.Id)).ToList();

        var (exportGetResult, freightGetResult) = (
            await _transportRepository.GetByIdAsync(new(request.ExportId), cancellationToken),
            await _freightRepository.GetManyByIdAsync(freightsId, cancellationToken));

        if (Result.Aggregate(exportGetResult, freightGetResult) is var result && result.IsFailure)
        {
            return result.Error;
        }

        var (export, freights) = (exportGetResult.Value, freightGetResult.Value);

        var releaseResults = freights.Select(f => FreightService.ReleaseFreight(f, export));

        if (Result.Aggregate(releaseResults) is var releaseResult && releaseResult.IsFailure)
        {
            return releaseResult.Error;
        }

        return _freightRepository.UpdateRange(freights);
    }
}
