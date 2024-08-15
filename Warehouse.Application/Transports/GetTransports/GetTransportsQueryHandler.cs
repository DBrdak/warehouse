using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Transports.GetTransports;

internal sealed class GetTransportsQueryHandler : IQueryHandler<GetTransportsQuery, List<TransportModel>>
{
    private readonly ITransportRepository _transportRepository;

    public GetTransportsQueryHandler(ITransportRepository transportRepository)
    {
        _transportRepository = transportRepository;
    }

    public async Task<Result<List<TransportModel>>> Handle(GetTransportsQuery request, CancellationToken cancellationToken)
    {
        var getTransportsResult = request.Type.ToLower() switch
        {
            "import" => await _transportRepository.GetAllImportsAsync(cancellationToken),
            "export" => await _transportRepository.GetAllExportsAsync(cancellationToken),
            _ => Result.Failure<List<Transport>>(Error.NullValue)
        };

        if (getTransportsResult.IsFailure)
        {
            return getTransportsResult.Error;
        }

        var transports = getTransportsResult.Value;

        return transports.Select(TransportModel.FromDomainModel).ToList();
    }
}
