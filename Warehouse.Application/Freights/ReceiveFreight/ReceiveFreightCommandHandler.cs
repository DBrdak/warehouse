using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Freights.Models;
using Warehouse.Domain.Freights;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Freights.ReceiveFreight;

internal sealed class ReceiveFreightCommandHandler : ICommandHandler<ReceiveFreightCommand, FreightModel>
{
    private readonly IFreightRepository _freightRepository;
    private readonly IPalletSpaceRepository _palletSpaceRepository;
    private readonly ITransportRepository _transportRepository;

    public ReceiveFreightCommandHandler(IFreightRepository freightRepository, IPalletSpaceRepository palletSpaceRepository, ITransportRepository transportRepository)
    {
        _freightRepository = freightRepository;
        _palletSpaceRepository = palletSpaceRepository;
        _transportRepository = transportRepository;
    }

    public async Task<Result<FreightModel>> Handle(ReceiveFreightCommand request, CancellationToken cancellationToken)
    {
        var (importGetResult, palletSpaceGetResult) = (
            await _transportRepository.GetByIdAsync(new TransportId(request.ImportId), cancellationToken),
            await _palletSpaceRepository.GetByIdAsync(new PalletSpaceId(request.PalletSpaceId), cancellationToken));

        if (Result.Aggregate(importGetResult, palletSpaceGetResult) is var result && result.IsFailure)
        {
            return result.Error;
        }

        var import = importGetResult.Value;
        var palletSpace = palletSpaceGetResult.Value;

        var freightCreateResult = FreightService.ReceiveFreight(
            import,
            palletSpace,
            request.Name,
            request.Type,
            request.Quantity,
            request.Unit);

        if (freightCreateResult.IsFailure)
        {
            return freightCreateResult.Error;
        }

        var freight = freightCreateResult.Value;

        var addResult = await _freightRepository.AddAsync(freight, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error;
        }

        return FreightModel.FromDomainModel(freight);
    }
}
