using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Transports.GetTransportDetails;

internal sealed class GetTransportDetailsQueryHandler : IQueryHandler<GetTransportDetailsQuery, TransportModel>
{
    private readonly ITransportRepository _transportRepository;

    public GetTransportDetailsQueryHandler(ITransportRepository transportRepository)
    {
        _transportRepository = transportRepository;
    }

    public async Task<Result<TransportModel>> Handle(
        GetTransportDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var getTransportResult =
            await _transportRepository.GetDetailedByIdAsync(new(request.TransportId), cancellationToken);

        if (getTransportResult.IsFailure)
        {
            return getTransportResult.Error;
        }

        var transport = getTransportResult.Value;

        return TransportModel.FromDomainModel(transport);
    }
}
