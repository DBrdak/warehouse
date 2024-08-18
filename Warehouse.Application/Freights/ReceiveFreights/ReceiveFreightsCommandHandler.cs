using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Freights;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Application.Freights.ReceiveFreights;

internal sealed class ReceiveFreightsCommandHandler : ICommandHandler<ReceiveFreightsCommand>
{
    private readonly IFreightRepository _freightRepository;
    private readonly IPalletSpaceRepository _palletSpaceRepository;
    private readonly ITransportRepository _transportRepository;

    public ReceiveFreightsCommandHandler(IFreightRepository freightRepository, IPalletSpaceRepository palletSpaceRepository, ITransportRepository transportRepository)
    {
        _freightRepository = freightRepository;
        _palletSpaceRepository = palletSpaceRepository;
        _transportRepository = transportRepository;
    }

    public async Task<Result> Handle(ReceiveFreightsCommand request, CancellationToken cancellationToken)
    {
        var importGetResult = await _transportRepository.GetByIdAsync(new TransportId(request.ImportId), cancellationToken);

        if (importGetResult.IsFailure)
        {
            return importGetResult.Error;
        }

        var import = importGetResult.Value;

        var freightsCreateResults = await Task.WhenAll(
            request.Freights.Select(
                async f => await CreateFreight(
                    f,
                    import)));

        if (Result.Aggregate(freightsCreateResults) is var result && result.IsFailure)
        {
            return result.Error;
        }

        var freights = freightsCreateResults.Select(r => r.Value).ToList();

        return await _freightRepository.AddRangeAsync(freights, cancellationToken);
    }

    private async Task<Result<Freight>> CreateFreight(FreightCreateModel createModel, Transport import)
    {
        var palletspaceGetResult = await _palletSpaceRepository.GetByDataAsync(
            PalletSpaceNumber.Create(createModel.PalletSpaceNumber).Value,
            Shelf.Create(createModel.ShelfNumber).Value,
            Rack.Create(createModel.RackNumber).Value,
            SectorNumber.Create(createModel.SectorNumber).Value);

        if (palletspaceGetResult.IsFailure)
        {
            return palletspaceGetResult.Error;
        }

        var palletSpace = palletspaceGetResult.Value;

        return FreightService.ReceiveFreight(
            import,
            palletSpace,
            createModel.Name,
            createModel.Type,
            createModel.Quantity,
            createModel.Unit);
    }
}
