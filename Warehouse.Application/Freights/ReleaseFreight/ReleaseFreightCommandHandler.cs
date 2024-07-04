using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Freights.Models;
using Warehouse.Application.Freights.ReleaseFreight;
using Warehouse.Domain.Freights;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace CHANGEME.ReleaseFreight;

internal sealed class ReleaseFreightCommandHandler : ICommandHandler<ReleaseFreightCommand, FreightModel>
{
    private readonly IFreightRepository _freightRepository;
    private readonly ITransportRepository _transportRepository;

    public ReleaseFreightCommandHandler(ITransportRepository transportRepository, IFreightRepository freightRepository)
    {
        _transportRepository = transportRepository;
        _freightRepository = freightRepository;
    }

    public async Task<Result<FreightModel>> Handle(ReleaseFreightCommand request, CancellationToken cancellationToken)
    {
        var (exportGetResult, freightGetResult) = (
            await _transportRepository.GetByIdAsync(new(request.ExportId), cancellationToken),
            await _freightRepository.GetByIdAsync(new(request.Id), cancellationToken));

        if (Result.Aggregate(exportGetResult, freightGetResult) is var result && result.IsFailure)
        {
            return result.Error;
        }

        var (export, freight) = (exportGetResult.Value, freightGetResult.Value);

        var releaseResult = FreightService.ReleaseFreight(freight, export);

        if (releaseResult.IsFailure)
        {
            return releaseResult.Error;
        }

        var updateResult = _freightRepository.Update(freight);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }

        freight = updateResult.Value;

        return FreightModel.FromDomainModel(freight);
    }
}
